using EduTrack.Core.Entities;
using EduTrack.Core.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace EduTrack.Infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        private readonly ApplicationDbContext _context;

        public ExcelService(ApplicationDbContext context)
        {
            _context = context;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<(bool exito, string mensaje, int registrosProcesados)> ProcesarArchivoExcelAsync(Stream archivo)
        {
            try
            {
                using var package = new ExcelPackage(archivo);
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension?.Rows ?? 0;

                if (rowCount <= 1)
                    return (false, "El archivo está vacío o no tiene datos válidos", 0);

                var registrosProcesados = 0;
                var errores = new List<string>();

                // Obtener datos maestros
                var tiposDocumento = await _context.TiposDocumento.ToDictionaryAsync(t => t.Codigo, t => t.Id);
                var modalidades = await _context.Modalidades.ToDictionaryAsync(m => m.Codigo, m => m.Id);
                var programas = await _context.ProgramasAcademicos.ToDictionaryAsync(p => p.Codigo, p => p.Id);

                for (int row = 2; row <= rowCount; row++) // Empezar en fila 2 (saltar encabezados)
                {
                    try
                    {
                        var numeroDocumento = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                        var tipoDocumento = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
                        var nombre = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                        var apellido = worksheet.Cells[row, 4].Value?.ToString()?.Trim();
                        var email = worksheet.Cells[row, 5].Value?.ToString()?.Trim();
                        var telefono = worksheet.Cells[row, 6].Value?.ToString()?.Trim();
                        var fechaNacimiento = worksheet.Cells[row, 7].Value;
                        var programaCodigo = worksheet.Cells[row, 8].Value?.ToString()?.Trim();
                        var modalidadCodigo = worksheet.Cells[row, 9].Value?.ToString()?.Trim();

                        if (string.IsNullOrEmpty(numeroDocumento) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email))
                            continue;

                        // Validar y convertir datos
                        if (!tiposDocumento.ContainsKey(tipoDocumento ?? "CC"))
                        {
                            errores.Add($"Fila {row}: Tipo de documento inválido");
                            continue;
                        }

                        if (!programas.ContainsKey(programaCodigo ?? "ING-SIS"))
                        {
                            errores.Add($"Fila {row}: Programa académico inválido");
                            continue;
                        }

                        if (!modalidades.ContainsKey(modalidadCodigo ?? "PRESENCIAL"))
                        {
                            errores.Add($"Fila {row}: Modalidad inválida");
                            continue;
                        }

                        var estudianteExistente = await _context.Estudiantes
                            .FirstOrDefaultAsync(e => e.NumeroDocumento == numeroDocumento);

                        if (estudianteExistente != null)
                        {
                            // Actualizar estudiante existente
                            estudianteExistente.Nombre = nombre;
                            estudianteExistente.Apellido = apellido;
                            estudianteExistente.Email = email;
                            estudianteExistente.Telefono = telefono;
                            estudianteExistente.TipoDocumentoId = tiposDocumento[tipoDocumento ?? "CC"];
                            estudianteExistente.ProgramaAcademicoId = programas[programaCodigo ?? "ING-SIS"];
                            estudianteExistente.ModalidadId = modalidades[modalidadCodigo ?? "PRESENCIAL"];
                        }
                        else
                        {
                            // Crear nuevo estudiante
                            var estudiante = new Estudiante
                            {
                                NumeroDocumento = numeroDocumento,
                                Nombre = nombre,
                                Apellido = apellido,
                                Email = email,
                                Telefono = telefono,
                                FechaNacimiento = DateTime.Now.AddYears(-18), // Valor por defecto
                                FechaIngreso = DateTime.Now,
                                Estado = "Activo",
                                SemestreActual = 1,
                                TipoDocumentoId = tiposDocumento[tipoDocumento ?? "CC"],
                                ProgramaAcademicoId = programas[programaCodigo ?? "ING-SIS"],
                                ModalidadId = modalidades[modalidadCodigo ?? "PRESENCIAL"]
                            };

                            _context.Estudiantes.Add(estudiante);
                        }

                        registrosProcesados++;
                    }
                    catch (Exception ex)
                    {
                        errores.Add($"Fila {row}: {ex.Message}");
                    }
                }

                await _context.SaveChangesAsync();

                var mensaje = registrosProcesados > 0 
                    ? $"Procesados {registrosProcesados} registros exitosamente"
                    : "No se procesaron registros";

                if (errores.Any())
                    mensaje += $". Errores: {string.Join(", ", errores.Take(5))}";

                return (true, mensaje, registrosProcesados);
            }
            catch (Exception ex)
            {
                return (false, $"Error procesando archivo: {ex.Message}", 0);
            }
        }

        public async Task<byte[]> GenerarTemplateExcelAsync()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Estudiantes");

            // Encabezados
            worksheet.Cells[1, 1].Value = "NumeroDocumento";
            worksheet.Cells[1, 2].Value = "TipoDocumento";
            worksheet.Cells[1, 3].Value = "Nombre";
            worksheet.Cells[1, 4].Value = "Apellido";
            worksheet.Cells[1, 5].Value = "Email";
            worksheet.Cells[1, 6].Value = "Telefono";
            worksheet.Cells[1, 7].Value = "FechaNacimiento";
            worksheet.Cells[1, 8].Value = "ProgramaCodigo";
            worksheet.Cells[1, 9].Value = "ModalidadCodigo";

            // Ejemplo de datos
            worksheet.Cells[2, 1].Value = "123456789";
            worksheet.Cells[2, 2].Value = "CC";
            worksheet.Cells[2, 3].Value = "Juan";
            worksheet.Cells[2, 4].Value = "Pérez";
            worksheet.Cells[2, 5].Value = "juan.perez@email.com";
            worksheet.Cells[2, 6].Value = "3001234567";
            worksheet.Cells[2, 7].Value = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
            worksheet.Cells[2, 8].Value = "ING-SIS";
            worksheet.Cells[2, 9].Value = "PRESENCIAL";

            // Autoajustar columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
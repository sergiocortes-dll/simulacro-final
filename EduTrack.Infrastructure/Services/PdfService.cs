using EduTrack.Core.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EduTrack.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        private readonly ApplicationDbContext _context;

        public PdfService(ApplicationDbContext context)
        {
            _context = context;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerarHojaAcademicaAsync(int estudianteId)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.TipoDocumento)
                .Include(e => e.Modalidad)
                .FirstOrDefaultAsync(e => e.Id == estudianteId);

            if (estudiante == null)
                throw new Exception("Estudiante no encontrado");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Encabezado
                    page.Header()
                        .AlignCenter()
                        .Text("EDUTRACK S.A.S.")
                        .FontSize(20)
                        .SemiBold().FontColor(Colors.Blue.Medium);

                    page.Header()
                        .AlignCenter()
                        .Text("HOJA ACADÉMICA DEL ESTUDIANTE")
                        .FontSize(16)
                        .SemiBold().ClampLines(10);

                    // Contenido
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(20);

                            // Información Personal
                            column.Item().Text("INFORMACIÓN PERSONAL").FontSize(14).SemiBold().Underline();
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Tipo de Documento: {estudiante.TipoDocumento.Nombre}");
                                    col.Item().Text($"Número de Documento: {estudiante.NumeroDocumento}");
                                    col.Item().Text($"Nombre: {estudiante.Nombre} {estudiante.Apellido}");
                                    col.Item().Text($"Email: {estudiante.Email}");
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Teléfono: {estudiante.Telefono}");
                                    col.Item().Text($"Fecha de Nacimiento: {estudiante.FechaNacimiento:yyyy-MM-dd}");
                                    col.Item().Text($"Fecha de Ingreso: {estudiante.FechaIngreso:yyyy-MM-dd}");
                                });
                            });

                            // Información Académica
                            column.Item().Text("INFORMACIÓN ACADÉMICA").FontSize(14).SemiBold().Underline();
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Programa Académico: {estudiante.ProgramaAcademico.Nombre}");
                                    col.Item().Text($"Modalidad: {estudiante.Modalidad.Nombre}");
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Semestre Actual: {estudiante.SemestreActual}");
                                    col.Item().Text($"Estado: {estudiante.Estado}");
                                });
                            });

                            // Observaciones
                            if (!string.IsNullOrEmpty(estudiante.Observaciones))
                            {
                                column.Item().Text("OBSERVACIONES").FontSize(14).SemiBold().Underline();
                                column.Item().Text(estudiante.Observaciones);
                            }
                        });

                    // Pie de página
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generado el: ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
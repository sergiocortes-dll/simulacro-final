using EduTrack.Core.Dtos;
using EduTrack.Core.Entities;
using EduTrack.Core.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.Infrastructure.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;

        public EstudianteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstudianteDto>> ObtenerTodosAsync()
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.TipoDocumento)
                .Include(e => e.Modalidad)
                .Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    NumeroDocumento = e.NumeroDocumento,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Email = e.Email,
                    Telefono = e.Telefono,
                    FechaNacimiento = e.FechaNacimiento,
                    FechaIngreso = e.FechaIngreso,
                    Estado = e.Estado,
                    SemestreActual = e.SemestreActual,
                    Observaciones = e.Observaciones,
                    ProgramaAcademico = e.ProgramaAcademico.Nombre,
                    TipoDocumento = e.TipoDocumento.Nombre,
                    Modalidad = e.Modalidad.Nombre
                })
                .ToListAsync();
        }

        public async Task<EstudianteDto?> ObtenerPorIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.TipoDocumento)
                .Include(e => e.Modalidad)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null) return null;

            return new EstudianteDto
            {
                Id = estudiante.Id,
                NumeroDocumento = estudiante.NumeroDocumento,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email = estudiante.Email,
                Telefono = estudiante.Telefono,
                FechaNacimiento = estudiante.FechaNacimiento,
                FechaIngreso = estudiante.FechaIngreso,
                Estado = estudiante.Estado,
                SemestreActual = estudiante.SemestreActual,
                Observaciones = estudiante.Observaciones,
                ProgramaAcademico = estudiante.ProgramaAcademico.Nombre,
                TipoDocumento = estudiante.TipoDocumento.Nombre,
                Modalidad = estudiante.Modalidad.Nombre
            };
        }

        public async Task<EstudianteDto> CrearAsync(CrearEstudianteDto dto)
        {
            var estudiante = new Estudiante
            {
                NumeroDocumento = dto.NumeroDocumento,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Telefono = dto.Telefono,
                FechaNacimiento = dto.FechaNacimiento,
                FechaIngreso = DateTime.Now,
                Estado = "Activo",
                SemestreActual = 1,
                Observaciones = dto.Observaciones,
                ProgramaAcademicoId = dto.ProgramaAcademicoId,
                TipoDocumentoId = dto.TipoDocumentoId,
                ModalidadId = dto.ModalidadId
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return await ObtenerPorIdAsync(estudiante.Id) ?? throw new Exception("Error al crear estudiante");
        }

        public async Task<EstudianteDto?> ActualizarAsync(ActualizarEstudianteDto dto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(dto.Id);
            if (estudiante == null) return null;

            estudiante.Nombre = dto.Nombre;
            estudiante.Apellido = dto.Apellido;
            estudiante.Email = dto.Email;
            estudiante.Telefono = dto.Telefono;
            estudiante.ProgramaAcademicoId = dto.ProgramaAcademicoId;
            estudiante.ModalidadId = dto.ModalidadId;
            estudiante.Estado = dto.Estado;
            estudiante.SemestreActual = dto.SemestreActual;
            estudiante.Observaciones = dto.Observaciones;

            await _context.SaveChangesAsync();

            return await ObtenerPorIdAsync(estudiante.Id);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return false;

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EstudianteDto?> ObtenerPorDocumentoAsync(string numeroDocumento)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.TipoDocumento)
                .Include(e => e.Modalidad)
                .FirstOrDefaultAsync(e => e.NumeroDocumento == numeroDocumento);

            if (estudiante == null) return null;

            return new EstudianteDto
            {
                Id = estudiante.Id,
                NumeroDocumento = estudiante.NumeroDocumento,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email = estudiante.Email,
                Telefono = estudiante.Telefono,
                FechaNacimiento = estudiante.FechaNacimiento,
                FechaIngreso = estudiante.FechaIngreso,
                Estado = estudiante.Estado,
                SemestreActual = estudiante.SemestreActual,
                Observaciones = estudiante.Observaciones,
                ProgramaAcademico = estudiante.ProgramaAcademico.Nombre,
                TipoDocumento = estudiante.TipoDocumento.Nombre,
                Modalidad = estudiante.Modalidad.Nombre
            };
        }

        public async Task<int> ContarEstudiantesAsync()
        {
            return await _context.Estudiantes.CountAsync();
        }

        public async Task<int> ContarEstudiantesActivosAsync()
        {
            return await _context.Estudiantes.CountAsync(e => e.Estado == "Activo");
        }

        public async Task<int> ContarEstudiantesPorModalidadAsync(int modalidadId)
        {
            return await _context.Estudiantes.CountAsync(e => e.ModalidadId == modalidadId);
        }

        public async Task<Dictionary<string, int>> ObtenerEstadisticasPorProgramaAsync()
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .GroupBy(e => e.ProgramaAcademico.Nombre)
                .Select(g => new { Programa = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(x => x.Programa, x => x.Cantidad);
        }
    }
}
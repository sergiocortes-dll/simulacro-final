using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Repositories;
using EduTrack.Infrastructure.Data;

namespace EduTrack.Infrastructure.Repositories
{
    // Implementación simple del repositorio con Entity Framework
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly EduTrackContext _context;

        public EstudianteRepository(EduTrackContext context)
        {
            _context = context;
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.Modalidad)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.Modalidad)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estudiante>> GetByProgramaAsync(int programaId)
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.Modalidad)
                .Where(e => e.ProgramaAcademicoId == programaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estudiante>> GetByEstadoAsync(string estado)
        {
            return await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .Include(e => e.Modalidad)
                .Where(e => e.Estado == estado)
                .ToListAsync();
        }

        public async Task<Estudiante> AddAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await GetByIdAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Estudiantes.AnyAsync(e => e.Id == id);
        }

        public async Task<int> GetTotalEstudiantesAsync()
        {
            return await _context.Estudiantes.CountAsync();
        }

        public async Task<int> GetEstudiantesActivosAsync()
        {
            return await _context.Estudiantes
                .CountAsync(e => e.Estado == "Activo");
        }

        public async Task<int> GetEstudiantesPorModalidadAsync(int modalidadId)
        {
            return await _context.Estudiantes
                .CountAsync(e => e.ModalidadId == modalidadId);
        }
    }
}
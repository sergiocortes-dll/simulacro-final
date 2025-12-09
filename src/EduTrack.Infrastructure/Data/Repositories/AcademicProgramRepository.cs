using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Interfaces;

namespace EduTrack.Infrastructure.Data.Repositories
{
    public class AcademicProgramRepository : IAcademicProgramRepository
    {
        private readonly ApplicationDbContext _context;

        public AcademicProgramRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AcademicProgram?> GetByIdAsync(int id)
        {
            return await _context.AcademicPrograms.FindAsync(id);
        }

        public async Task<IEnumerable<AcademicProgram>> GetAllAsync()
        {
            return await _context.AcademicPrograms.ToListAsync();
        }

        public async Task AddAsync(AcademicProgram program)
        {
            await _context.AcademicPrograms.AddAsync(program);
        }

        public async Task UpdateAsync(AcademicProgram program)
        {
            _context.AcademicPrograms.Update(program);
        }

        public async Task DeleteAsync(AcademicProgram program)
        {
            _context.AcademicPrograms.Remove(program);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.AcademicPrograms.AnyAsync(p => p.Id == id);
        }

        // Métodos específicos para AcademicProgram
        public async Task<AcademicProgram?> GetByCodeAsync(string code)
        {
            return await _context.AcademicPrograms
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IEnumerable<AcademicProgram>> GetActiveProgramsAsync()
        {
            return await _context.AcademicPrograms
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<AcademicProgram>> GetByFacultyAsync(string faculty)
        {
            return await _context.AcademicPrograms
                .Where(p => p.Faculty == faculty)
                .ToListAsync();
        }

        public async Task<IEnumerable<AcademicProgram>> GetByModalityAsync(string modality)
        {
            return await _context.AcademicPrograms
                .Where(p => p.Modality == modality)
                .ToListAsync();
        }

        public async Task<int> GetStudentCountByProgramAsync(int programId)
        {
            return await _context.Students
                .CountAsync(s => s.AcademicProgramId == programId && s.IsActive);
        }

        public async Task<bool> CodeExistsAsync(string code)
        {
            return await _context.AcademicPrograms
                .AnyAsync(p => p.Code == code);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.AcademicPrograms
                .AnyAsync(p => p.Name == name);
        }
    }
}
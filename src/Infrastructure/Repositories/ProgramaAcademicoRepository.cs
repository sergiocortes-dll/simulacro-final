using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Repositories;
using EduTrack.Infrastructure.Data;

namespace EduTrack.Infrastructure.Repositories
{
    public class ProgramaAcademicoRepository : IProgramaAcademicoRepository
    {
        private readonly EduTrackContext _context;

        public ProgramaAcademicoRepository(EduTrackContext context)
        {
            _context = context;
        }

        public async Task<ProgramaAcademico> GetByIdAsync(int id)
        {
            return await _context.ProgramasAcademicos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProgramaAcademico>> GetAllAsync()
        {
            return await _context.ProgramasAcademicos
                .ToListAsync();
        }

        public async Task<ProgramaAcademico> AddAsync(ProgramaAcademico programa)
        {
            _context.ProgramasAcademicos.Add(programa);
            await _context.SaveChangesAsync();
            return programa;
        }

        public async Task UpdateAsync(ProgramaAcademico programa)
        {
            _context.ProgramasAcademicos.Update(programa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var programa = await GetByIdAsync(id);
            if (programa != null)
            {
                _context.ProgramasAcademicos.Remove(programa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ProgramasAcademicos
                .AnyAsync(p => p.Id == id);
        }
    }
}
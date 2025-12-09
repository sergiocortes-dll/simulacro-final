using System.Collections.Generic;
using System.Threading.Tasks;
using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Repositories
{
    public interface IProgramaAcademicoRepository
    {
        Task<ProgramaAcademico> GetByIdAsync(int id);
        Task<IEnumerable<ProgramaAcademico>> GetAllAsync();
        Task<ProgramaAcademico> AddAsync(ProgramaAcademico programa);
        Task UpdateAsync(ProgramaAcademico programa);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
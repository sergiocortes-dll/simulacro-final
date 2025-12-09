using System.Collections.Generic;
using System.Threading.Tasks;
using EduTrack.Domain.Entities;

namespace EduTrack.Domain.Repositories
{
    // Interfaz simple para el repositorio - concepto DDD básico
    public interface IEstudianteRepository
    {
        Task<Estudiante> GetByIdAsync(int id);
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<IEnumerable<Estudiante>> GetByProgramaAsync(int programaId);
        Task<IEnumerable<Estudiante>> GetByEstadoAsync(string estado);
        Task<Estudiante> AddAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalEstudiantesAsync();
        Task<int> GetEstudiantesActivosAsync();
        Task<int> GetEstudiantesPorModalidadAsync(int modalidadId);
    }
}
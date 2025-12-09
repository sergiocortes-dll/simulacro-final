using EduTrack.Core.Dtos;
using EduTrack.Core.Entities;

namespace EduTrack.Core.Interfaces
{
    public interface IEstudianteService
    {
        Task<List<EstudianteDto>> ObtenerTodosAsync();
        Task<EstudianteDto?> ObtenerPorIdAsync(int id);
        Task<EstudianteDto> CrearAsync(CrearEstudianteDto dto);
        Task<EstudianteDto?> ActualizarAsync(ActualizarEstudianteDto dto);
        Task<bool> EliminarAsync(int id);
        Task<EstudianteDto?> ObtenerPorDocumentoAsync(string numeroDocumento);
        Task<int> ContarEstudiantesAsync();
        Task<int> ContarEstudiantesActivosAsync();
        Task<int> ContarEstudiantesPorModalidadAsync(int modalidadId);
        Task<Dictionary<string, int>> ObtenerEstadisticasPorProgramaAsync();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Repositories;

namespace EduTrack.Application.Services
{
    // Servicio de aplicación simple para manejar la lógica de estudiantes
    public class EstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IProgramaAcademicoRepository _programaRepository;

        public EstudianteService(
            IEstudianteRepository estudianteRepository,
            IProgramaAcademicoRepository programaRepository)
        {
            _estudianteRepository = estudianteRepository;
            _programaRepository = programaRepository;
        }

        public async Task<Estudiante> CrearEstudianteAsync(Estudiante estudiante)
        {
            // Validaciones simples
            if (string.IsNullOrWhiteSpace(estudiante.Nombre))
                throw new ArgumentException("El nombre es requerido");
            
            if (string.IsNullOrWhiteSpace(estudiante.Email))
                throw new ArgumentException("El email es requerido");
            
            // Verificar que el programa académico existe
            var programaExists = await _programaRepository.ExistsAsync(estudiante.ProgramaAcademicoId);
            if (!programaExists)
                throw new ArgumentException("El programa académico no existe");
            
            return await _estudianteRepository.AddAsync(estudiante);
        }

        public async Task<Estudiante> ObtenerEstudianteAsync(int id)
        {
            return await _estudianteRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Estudiante>> ObtenerTodosEstudiantesAsync()
        {
            return await _estudianteRepository.GetAllAsync();
        }

        public async Task<Estudiante> ActualizarEstudianteAsync(Estudiante estudiante)
        {
            var exists = await _estudianteRepository.ExistsAsync(estudiante.Id);
            if (!exists)
                throw new KeyNotFoundException("El estudiante no existe");
            
            await _estudianteRepository.UpdateAsync(estudiante);
            return estudiante;
        }

        public async Task EliminarEstudianteAsync(int id)
        {
            var exists = await _estudianteRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException("El estudiante no existe");
            
            await _estudianteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Estudiante>> ObtenerEstudiantesPorProgramaAsync(int programaId)
        {
            return await _estudianteRepository.GetByProgramaAsync(programaId);
        }

        public async Task<IEnumerable<Estudiante>> ObtenerEstudiantesPorEstadoAsync(string estado)
        {
            return await _estudianteRepository.GetByEstadoAsync(estado);
        }

        // Métodos para el dashboard
        public async Task<int> ObtenerTotalEstudiantesAsync()
        {
            return await _estudianteRepository.GetTotalEstudiantesAsync();
        }

        public async Task<int> ObtenerEstudiantesActivosAsync()
        {
            return await _estudianteRepository.GetEstudiantesActivosAsync();
        }

        public async Task<int> ObtenerEstudiantesPorModalidadAsync(int modalidadId)
        {
            return await _estudianteRepository.GetEstudiantesPorModalidadAsync(modalidadId);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using EduTrack.Domain.Entities;
using EduTrack.Domain.Repositories;

namespace EduTrack.Application.Services
{
    public class ProgramaAcademicoService
    {
        private readonly IProgramaAcademicoRepository _programaRepository;

        public ProgramaAcademicoService(IProgramaAcademicoRepository programaRepository)
        {
            _programaRepository = programaRepository;
        }

        public async Task<ProgramaAcademico> CrearProgramaAsync(ProgramaAcademico programa)
        {
            if (string.IsNullOrWhiteSpace(programa.Nombre))
                throw new ArgumentException("El nombre del programa es requerido");
            
            if (string.IsNullOrWhiteSpace(programa.Codigo))
                throw new ArgumentException("El código del programa es requerido");
            
            return await _programaRepository.AddAsync(programa);
        }

        public async Task<IEnumerable<ProgramaAcademico>> ObtenerTodosProgramasAsync()
        {
            return await _programaRepository.GetAllAsync();
        }

        public async Task<ProgramaAcademico> ObtenerProgramaAsync(int id)
        {
            return await _programaRepository.GetByIdAsync(id);
        }

        public async Task<ProgramaAcademico> ActualizarProgramaAsync(ProgramaAcademico programa)
        {
            var exists = await _programaRepository.ExistsAsync(programa.Id);
            if (!exists)
                throw new KeyNotFoundException("El programa no existe");
            
            await _programaRepository.UpdateAsync(programa);
            return programa;
        }

        public async Task EliminarProgramaAsync(int id)
        {
            var exists = await _programaRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException("El programa no existe");
            
            await _programaRepository.DeleteAsync(id);
        }
    }
}
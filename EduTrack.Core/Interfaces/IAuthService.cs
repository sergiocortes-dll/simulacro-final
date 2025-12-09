using EduTrack.Core.Dtos;

namespace EduTrack.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<AuthResponseDto?> RegistroEstudianteAsync(RegistroDto dto);
        Task<EstudianteDto?> ObtenerEstudiantePorTokenAsync(string token);
    }
}
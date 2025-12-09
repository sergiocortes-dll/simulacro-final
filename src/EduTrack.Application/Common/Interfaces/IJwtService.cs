using EduTrack.Application.Common.DTOs;
using EduTrack.Domain.Entities;

namespace EduTrack.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(Student student);
    string GenerateRefreshToken();
    Task<AuthResponseDto> AuthenticateStudentAsync(string documentNumber, string email, string password);
    Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken);
    Task<bool> ValidateTokenAsync(string token);
    Task RevokeTokenAsync(string token);
}
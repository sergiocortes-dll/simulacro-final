namespace EduTrack.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailConfirmacionRegistroAsync(string email, string nombre, string numeroDocumento);
        Task<bool> EnviarEmailAsync(string destinatario, string asunto, string mensaje);
    }
}
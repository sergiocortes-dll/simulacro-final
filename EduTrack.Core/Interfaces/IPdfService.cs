namespace EduTrack.Core.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> GenerarHojaAcademicaAsync(int estudianteId);
    }
}
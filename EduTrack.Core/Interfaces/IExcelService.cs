namespace EduTrack.Core.Interfaces
{
    public interface IExcelService
    {
        Task<(bool exito, string mensaje, int registrosProcesados)> ProcesarArchivoExcelAsync(Stream archivo);
        Task<byte[]> GenerarTemplateExcelAsync();
    }
}
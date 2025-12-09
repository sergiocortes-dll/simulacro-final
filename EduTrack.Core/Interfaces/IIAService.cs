using EduTrack.Core.Dtos;

namespace EduTrack.Core.Interfaces
{
    public interface IIAService
    {
        Task<IAResponseDto> ProcesarConsultaAsync(string pregunta);
        Task<List<DashboardCardDto>> ObtenerTarjetasDashboardAsync();
    }
}
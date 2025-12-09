namespace EduTrack.Core.Dtos
{
    public class DashboardCardDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string Icono { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    public class IAResponseDto
    {
        public string Pregunta { get; set; } = string.Empty;
        public string Respuesta { get; set; } = string.Empty;
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
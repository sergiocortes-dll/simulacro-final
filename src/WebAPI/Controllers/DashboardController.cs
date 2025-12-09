using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EduTrack.Application.Services;

namespace EduTrack.WebAPI.Controllers
{
    // Controlador para el dashboard con métricas
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly EstudianteService _estudianteService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(
            EstudianteService estudianteService,
            ILogger<DashboardController> logger)
        {
            _estudianteService = estudianteService;
            _logger = logger;
        }

        // GET: api/dashboard/metricas
        [HttpGet("metricas")]
        public async Task<ActionResult<object>> GetMetricas()
        {
            try
            {
                var totalEstudiantes = await _estudianteService.ObtenerTotalEstudiantesAsync();
                var estudiantesActivos = await _estudianteService.ObtenerEstudiantesActivosAsync();
                var estudiantesPresencial = await _estudianteService.ObtenerEstudiantesPorModalidadAsync(1);
                var estudiantesVirtual = await _estudianteService.ObtenerEstudiantesPorModalidadAsync(2);

                var metricas = new
                {
                    TotalEstudiantes = totalEstudiantes,
                    EstudiantesActivos = estudiantesActivos,
                    EstudiantesPresencial = estudiantesPresencial,
                    EstudiantesVirtual = estudiantesVirtual,
                    PorcentajeActivos = totalEstudiantes > 0 ? (estudiantesActivos * 100) / totalEstudiantes : 0
                };

                return Ok(metricas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener métricas del dashboard");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
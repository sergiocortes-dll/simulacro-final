using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EduTrack.Application.Services;
using EduTrack.Domain.Entities;

namespace EduTrack.WebAPI.Controllers
{
    // Controlador REST API para programas académicos (endpoints públicos)
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramasController : ControllerBase
    {
        private readonly ProgramaAcademicoService _programaService;
        private readonly ILogger<ProgramasController> _logger;

        public ProgramasController(
            ProgramaAcademicoService programaService,
            ILogger<ProgramasController> logger)
        {
            _programaService = programaService;
            _logger = logger;
        }

        // GET: api/programas - Endpoint público para estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramaAcademico>>> GetProgramas()
        {
            try
            {
                var programas = await _programaService.ObtenerTodosProgramasAsync();
                return Ok(programas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener programas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/programas/5 - Endpoint público
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramaAcademico>> GetPrograma(int id)
        {
            try
            {
                var programa = await _programaService.ObtenerProgramaAsync(id);
                
                if (programa == null)
                {
                    return NotFound();
                }
                
                return Ok(programa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener programa {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EduTrack.Application.Services;
using EduTrack.Domain.Entities;

namespace EduTrack.WebAPI.Controllers
{
    // Controlador simple REST API para estudiantes
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly EstudianteService _estudianteService;
        private readonly ILogger<EstudiantesController> _logger;

        public EstudiantesController(
            EstudianteService estudianteService,
            ILogger<EstudiantesController> logger)
        {
            _estudianteService = estudianteService;
            _logger = logger;
        }

        // GET: api/estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            try
            {
                var estudiantes = await _estudianteService.ObtenerTodosEstudiantesAsync();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiantes");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            try
            {
                var estudiante = await _estudianteService.ObtenerEstudianteAsync(id);
                
                if (estudiante == null)
                {
                    return NotFound();
                }
                
                return Ok(estudiante);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiante {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/estudiantes
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            try
            {
                var nuevoEstudiante = await _estudianteService.CrearEstudianteAsync(estudiante);
                return CreatedAtAction(nameof(GetEstudiante), 
                    new { id = nuevoEstudiante.Id }, nuevoEstudiante);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear estudiante");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/estudiantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return BadRequest("El ID del estudiante no coincide");
            }

            try
            {
                await _estudianteService.ActualizarEstudianteAsync(estudiante);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estudiante {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            try
            {
                await _estudianteService.EliminarEstudianteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar estudiante {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/estudiantes/programa/1
        [HttpGet("programa/{programaId}")]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantesPorPrograma(int programaId)
        {
            try
            {
                var estudiantes = await _estudianteService.ObtenerEstudiantesPorProgramaAsync(programaId);
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiantes por programa {ProgramaId}", programaId);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/estudiantes/estado/activo
        [HttpGet("estado/{estado}")]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantesPorEstado(string estado)
        {
            try
            {
                var estudiantes = await _estudianteService.ObtenerEstudiantesPorEstadoAsync(estado);
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estudiantes por estado {Estado}", estado);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
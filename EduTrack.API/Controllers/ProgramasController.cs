using EduTrack.Core.Entities;
using EduTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProgramas()
        {
            try
            {
                var programas = await _context.ProgramasAcademicos
                    .Where(p => p.Estado)
                    .Select(p => new
                    {
                        p.Id,
                        p.Nombre,
                        p.Codigo,
                        p.Descripcion,
                        p.DuracionSemestres,
                        p.Nivel
                    })
                    .ToListAsync();

                return Ok(programas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error en el servidor: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPrograma(int id)
        {
            try
            {
                var programa = await _context.ProgramasAcademicos
                    .FirstOrDefaultAsync(p => p.Id == id && p.Estado);

                if (programa == null)
                    return NotFound(new { mensaje = "Programa no encontrado" });

                return Ok(programa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error en el servidor: {ex.Message}" });
            }
        }
    }
}
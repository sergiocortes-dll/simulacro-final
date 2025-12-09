using EduTrack.Core.Dtos;
using EduTrack.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            try
            {
                var resultado = await _authService.RegistroEstudianteAsync(dto);
                if (resultado == null)
                    return BadRequest(new { mensaje = "Error en el registro. El email o documento ya existe." });

                // Enviar email de confirmación
                await _emailService.EnviarEmailConfirmacionRegistroAsync(dto.Email, dto.Nombre, dto.NumeroDocumento);

                return Ok(new
                {
                    mensaje = "Registro exitoso. Se ha enviado un email de confirmación.",
                    token = resultado.Token,
                    expiracion = resultado.Expiracion
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error en el servidor: {ex.Message}" });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var resultado = await _authService.LoginAsync(dto);
                if (resultado == null)
                    return Unauthorized(new { mensaje = "Credenciales inválidas" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error en el servidor: {ex.Message}" });
            }
        }

        [HttpGet("perfil")]
        [Authorize]
        public async Task<IActionResult> ObtenerPerfil()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var estudiante = await _authService.ObtenerEstudiantePorTokenAsync(token);
                
                if (estudiante == null)
                    return NotFound(new { mensaje = "Estudiante no encontrado" });

                return Ok(estudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error en el servidor: {ex.Message}" });
            }
        }
    }
}
using EduTrack.Core.Dtos;
using EduTrack.Core.Interfaces;
using EduTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EduTrack.Infrastructure.Services
{
    public class IAService : IIAService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IAService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<DashboardCardDto>> ObtenerTarjetasDashboardAsync()
        {
            var totalEstudiantes = await _context.Estudiantes.CountAsync();
            var estudiantesActivos = await _context.Estudiantes.CountAsync(e => e.Estado == "Activo");
            var estudiantesVirtuales = await _context.Estudiantes.CountAsync(e => e.Modalidad.Codigo == "VIRTUAL");
            var estudiantesPresenciales = await _context.Estudiantes.CountAsync(e => e.Modalidad.Codigo == "PRESENCIAL");

            var estudiantesPorPrograma = await _context.Estudiantes
                .Include(e => e.ProgramaAcademico)
                .GroupBy(e => e.ProgramaAcademico.Nombre)
                .Select(g => new { Programa = g.Key, Cantidad = g.Count() })
                .ToListAsync();

            var programaConMasEstudiantes = estudiantesPorPrograma
                .OrderByDescending(p => p.Cantidad)
                .FirstOrDefault();

            return new List<DashboardCardDto>
            {
                new DashboardCardDto
                {
                    Titulo = "Total Estudiantes",
                    Valor = totalEstudiantes.ToString(),
                    Icono = "fas fa-users",
                    Color = "bg-primary"
                },
                new DashboardCardDto
                {
                    Titulo = "Estudiantes Activos",
                    Valor = estudiantesActivos.ToString(),
                    Icono = "fas fa-user-check",
                    Color = "bg-success"
                },
                new DashboardCardDto
                {
                    Titulo = "Modalidad Virtual",
                    Valor = estudiantesVirtuales.ToString(),
                    Icono = "fas fa-laptop",
                    Color = "bg-info"
                },
                new DashboardCardDto
                {
                    Titulo = "Programa Principal",
                    Valor = programaConMasEstudiantes?.Programa ?? "N/A",
                    Icono = "fas fa-graduation-cap",
                    Color = "bg-warning"
                }
            };
        }

        public async Task<IAResponseDto> ProcesarConsultaAsync(string pregunta)
        {
            var respuesta = await GenerarRespuestaIA(pregunta);
            
            return new IAResponseDto
            {
                Pregunta = pregunta,
                Respuesta = respuesta,
                FechaConsulta = DateTime.Now
            };
        }

        private async Task<string> GenerarRespuestaIA(string pregunta)
        {
            // En una implementación real, aquí se conectaría con una API de IA como OpenAI, Gemini, etc.
            // Por ahora, usaremos una lógica simple basada en palabras clave

            var preguntaLower = pregunta.ToLower();
            var respuesta = string.Empty;

            if (preguntaLower.Contains("cuántos estudiantes") && preguntaLower.Contains("total"))
            {
                var total = await _context.Estudiantes.CountAsync();
                respuesta = $"Actualmente tenemos {total} estudiantes registrados en el sistema.";
            }
            else if (preguntaLower.Contains("activos"))
            {
                var activos = await _context.Estudiantes.CountAsync(e => e.Estado == "Activo");
                respuesta = $"Hay {activos} estudiantes activos actualmente.";
            }
            else if (preguntaLower.Contains("virtual"))
            {
                var virtuales = await _context.Estudiantes.CountAsync(e => e.Modalidad.Codigo == "VIRTUAL");
                respuesta = $"{virtuales} estudiantes están en modalidad virtual.";
            }
            else if (preguntaLower.Contains("presencial"))
            {
                var presenciales = await _context.Estudiantes.CountAsync(e => e.Modalidad.Codigo == "PRESENCIAL");
                respuesta = $"{presenciales} estudiantes están en modalidad presencial.";
            }
            else if (preguntaLower.Contains("programa") || preguntaLower.Contains("carrera"))
            {
                var estadisticas = await _context.Estudiantes
                    .Include(e => e.ProgramaAcademico)
                    .GroupBy(e => e.ProgramaAcademico.Nombre)
                    .Select(g => new { Programa = g.Key, Cantidad = g.Count() })
                    .OrderByDescending(p => p.Cantidad)
                    .Take(3)
                    .ToListAsync();

                respuesta = "Distribución de estudiantes por programa:\n";
                foreach (var stat in estadisticas)
                {
                    respuesta += $"- {stat.Programa}: {stat.Cantidad} estudiantes\n";
                }
            }
            else if (preguntaLower.Contains("semestre"))
            {
                var estadisticas = await _context.Estudiantes
                    .GroupBy(e => e.SemestreActual)
                    .Select(g => new { Semestre = g.Key, Cantidad = g.Count() })
                    .OrderBy(s => s.Semestre)
                    .ToListAsync();

                respuesta = "Distribución de estudiantes por semestre:\n";
                foreach (var stat in estadisticas.Take(5))
                {
                    respuesta += $"- Semestre {stat.Semestre}: {stat.Cantidad} estudiantes\n";
                }
            }
            else
            {
                respuesta = "Lo siento, no puedo responder esa pregunta específica. Por favor, intenta con preguntas como:\n" +
                           "- ¿Cuántos estudiantes hay en total?\n" +
                           "- ¿Cuántos estudiantes están activos?\n" +
                           "- ¿Cuántos estudiantes están en modalidad virtual?\n" +
                           "- ¿Cuántos estudiantes hay por programa?";
            }

            return respuesta;
        }
    }
}
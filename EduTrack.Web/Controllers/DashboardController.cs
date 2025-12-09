using EduTrack.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DashboardController : Controller
    {
        private readonly IIAService _iaService;
        private readonly IEstudianteService _estudianteService;

        public DashboardController(IIAService iaService, IEstudianteService estudianteService)
        {
            _iaService = iaService;
            _estudianteService = estudianteService;
        }

        public async Task<IActionResult> Index()
        {
            var tarjetas = await _iaService.ObtenerTarjetasDashboardAsync();
            return View(tarjetas);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarIA(string pregunta)
        {
            if (string.IsNullOrEmpty(pregunta))
            {
                return Json(new { exito = false, mensaje = "Por favor ingrese una pregunta" });
            }

            try
            {
                var respuesta = await _iaService.ProcesarConsultaAsync(pregunta);
                return Json(new { exito = true, respuesta });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = $"Error procesando consulta: {ex.Message}" });
            }
        }
    }
}
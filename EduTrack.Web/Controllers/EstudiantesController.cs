using EduTrack.Core.Dtos;
using EduTrack.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrack.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EstudiantesController : Controller
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IExcelService _excelService;
        private readonly IPdfService _pdfService;

        public EstudiantesController(
            IEstudianteService estudianteService,
            IExcelService excelService,
            IPdfService pdfService)
        {
            _estudianteService = estudianteService;
            _excelService = excelService;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            var estudiantes = await _estudianteService.ObtenerTodosAsync();
            return View(estudiantes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearEstudianteDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _estudianteService.CrearAsync(dto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error creando estudiante: {ex.Message}");
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var estudiante = await _estudianteService.ObtenerPorIdAsync(id);
            if (estudiante == null)
                return NotFound();

            var dto = new ActualizarEstudianteDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Email = estudiante.Email,
                Telefono = estudiante.Telefono,
                Estado = estudiante.Estado,
                SemestreActual = estudiante.SemestreActual,
                Observaciones = estudiante.Observaciones
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ActualizarEstudianteDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await _estudianteService.ActualizarAsync(dto);
                    if (resultado != null)
                        return RedirectToAction(nameof(Index));
                    
                    ModelState.AddModelError(string.Empty, "Estudiante no encontrado");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error actualizando estudiante: {ex.Message}");
                }
            }
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _estudianteService.EliminarAsync(id);
                if (resultado)
                    return RedirectToAction(nameof(Index));
                
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error eliminando estudiante: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DescargarHojaAcademica(int id)
        {
            try
            {
                var pdfBytes = await _pdfService.GenerarHojaAcademicaAsync(id);
                return File(pdfBytes, "application/pdf", $"HojaAcademica_{id}.pdf");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error generando PDF: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile archivoExcel)
        {
            if (archivoExcel == null || archivoExcel.Length == 0)
            {
                TempData["Error"] = "Por favor seleccione un archivo Excel";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                using var stream = archivoExcel.OpenReadStream();
                var (exito, mensaje, registrosProcesados) = await _excelService.ProcesarArchivoExcelAsync(stream);
                
                if (exito)
                {
                    TempData["Success"] = mensaje;
                }
                else
                {
                    TempData["Error"] = mensaje;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error procesando archivo: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DescargarTemplateExcel()
        {
            var templateBytes = await _excelService.GenerarTemplateExcelAsync();
            return File(templateBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template_Estudiantes.xlsx");
        }
    }
}
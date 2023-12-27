using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class FormularioController : Controller
    {
        private readonly IFormularioBusiness _formularioService;
        private readonly ILogger<FormularioController> _logger;

        public FormularioController(IFormularioBusiness formularioService, ILogger<FormularioController> logger)
        {
            _formularioService = formularioService;
            _logger = logger;
        }

        public IActionResult MostrarFormulario()
        {
            var viewModel = new FormularioViewModel();
            _formularioService.ConfigurarFormularioViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MostrarFormulario(FormularioViewModel viewModel)
        {
            try
            {
                // Realizar la acción sin validar el modelo
                if (await _formularioService.EnviarConsulta(viewModel))
                {
                    return RedirectToAction("ConsultaEnviada");
                }

                _logger.LogError("Error al enviar el formulario");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la acción MostrarFormulario: {ex.Message}");
            }

            _formularioService.ConfigurarFormularioViewModel(viewModel);
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult ObtenerProvinciasFiltradas(string pais)
        {
            var provinciasFiltradas = _formularioService.ObtenerProvinciasFiltradas(pais);
            return Json(provinciasFiltradas);
        }

        [HttpPost]
        public IActionResult ObtenerLocalidadesFiltradas(string provincia)
        {
            var localidadesFiltradas = _formularioService.ObtenerLocalidadesFiltradas(provincia);
            return Json(localidadesFiltradas);
        }



        [HttpPost]
        public IActionResult ObtenerLocacionesFiltradas(string provincia)
        {
            var locacionesFiltradas = _formularioService.ObtenerLocacionesFiltradas(provincia);
            return Json(locacionesFiltradas);
        }

        [HttpGet]
        public IActionResult ConsultaEnviada()
        {
            return View();
        }
    }
}

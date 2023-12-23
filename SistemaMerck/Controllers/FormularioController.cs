using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Modelos.ViewModels;
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
            if (await _formularioService.EnviarConsulta(viewModel))
            {
                return RedirectToAction("ConsultaEnviada");
            }

            // Maneja el error de alguna manera, por ejemplo, muestra un mensaje de error
            _logger.LogError("Error al enviar el formulario");

            // Si llegamos aquí, hay un problema, así que volvemos a mostrar el formulario
            _formularioService.ConfigurarFormularioViewModel(viewModel);
            return View(viewModel);
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

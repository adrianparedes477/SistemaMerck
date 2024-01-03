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
            if (ModelState.IsValid)
            {
                
                // Enviar la consulta con los datos del formulario y la URL
                if (await _formularioService.EnviarConsulta(viewModel))
                {
                    return RedirectToAction("ConsultaEnviada");
                }

                _logger.LogError("Error al enviar el formulario");
            }

            _formularioService.ConfigurarFormularioViewModel(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ObtenerProvinciasFiltradas(string pais)
        {
            var provinciasFiltradas = _formularioService.ObtenerProvinciasFiltradas(pais);
            return Json(new { provincias = provinciasFiltradas });
        }


        [HttpPost]
        public IActionResult ObtenerLocalidadesFiltradas(string provincia)
        {
            var localidadesFiltradas = _formularioService.ObtenerLocalidadesFiltradas(provincia);
            return Json(new { localidades = localidadesFiltradas });
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

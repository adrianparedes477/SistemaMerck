using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Helpers.Interface;
using SistemaMerck.Models;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using System.Diagnostics;
using SistemaMerck.Helpers;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocacionService _locacionService;
        private readonly ICorreoService _correoService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            LocacionService locacionService,
            ICorreoService correoService,
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _locacionService = locacionService;
            _correoService = correoService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.MapsKey = _configuration.GetSection("MicrosoftMaps:ApiKey").Value;
            var modelo = new UsuarioVM
            {
                Edades = Enumerable.Range(8, 50).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList()
            };
            return View(modelo);
        }

        [HttpPost]
        public IActionResult Pantalla2(UsuarioVM viewModel)
        {
            if (viewModel.EdadPrimeraMentruacion > viewModel.EdadActual)
            {
                ModelState.AddModelError("EdadPrimeraMentruacion", "La Edad Primera Menstruación no puede ser mayor a la Edad Actual.");
                viewModel.Edades = Enumerable.Range(8, 50).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
                return View("Index", viewModel);
            }

            var usuarioDto = new UsuarioDto
            {
                EdadActual = viewModel.EdadActual,
                EdadPrimeraMentruacion = viewModel.EdadPrimeraMentruacion
            };

            var reservaOvarica = CalcularReservaOvarica(usuarioDto);

            var usuario = new UsuarioVM
            {
                EdadActual = viewModel.EdadActual,
                EdadPrimeraMentruacion = viewModel.EdadPrimeraMentruacion,
                ReservaOvarica = reservaOvarica
            };

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Pantalla3()
        {
            var viewModel = new UsuarioVM();
            return View();
        }

        [HttpGet]
        public IActionResult FormularioContacto()
        {
            return PartialView("_FormularioContacto");
        }

        [HttpPost]
        public async Task<IActionResult> EnviarFormulario(UsuarioVM viewModel)
        {
            try
            {
                string destinatario = "adrianjparedes477@gmail.com";
                string asunto = viewModel.AsuntoDelCorreo;
                string cuerpo = viewModel.CuerpoDelCorreo;

                await _correoService.EnviarCorreoAsync(destinatario, asunto, cuerpo).ConfigureAwait(false);

                TempData["exitoso"] = "Formulario enviado con éxito";

                return RedirectToAction("Pantalla3", viewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Ocurrió un error al enviar el formulario. Por favor, inténtalo de nuevo.";

                return RedirectToAction("Pantalla3", viewModel);
            }
        }

        [HttpGet]
        public IActionResult ObtenerLocacionesJson()
        {
            var locacionesDto = _locacionService.ObtenerLocaciones();
            return Json(locacionesDto);
        }

        private double CalcularReservaOvarica(UsuarioDto usuarioDto)
        {
            return (usuarioDto.EdadActual + usuarioDto.EdadPrimeraMentruacion) / 2.0;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

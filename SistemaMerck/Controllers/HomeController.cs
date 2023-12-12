using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using SistemaMerck.Models;
using System.Diagnostics;
using SistemaMerck.Helpers;
using Microsoft.AspNetCore.Builder.Extensions;
using SistemaMerck.Helpers.Interface;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocacionService _locacionService;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICorreoService _correoService;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, LocacionService locacionService, ICorreoService correoService)
        {
            _logger = logger;
            _configuration = configuration;
            _locacionService = locacionService;
            _correoService = correoService;
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

        public IActionResult Pantalla3()
        {
            var viewModel = new UsuarioVM();
            return View();
        }

        public IActionResult ObtenerLocacionesJson()
        {
            var csvFilePath = "https://raw.githubusercontent.com/adrianparedes477/Archivo/main/clinicas.csv";
            var locaciones = _locacionService.ObtenerLocacionesDesdeCSV(csvFilePath);
            var locacionesDto = _locacionService.ConvertirLocacionesALocacionDto(locaciones);
            return Json(locacionesDto);
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

                // Resto de la lógica después de enviar el formulario, si es necesario

                return RedirectToAction("Pantalla3", viewModel);
            }
            catch (Exception ex)
            {
                // Manejo de errores según tus necesidades
                TempData["error"] = "Ocurrió un error al enviar el formulario. Por favor, inténtalo de nuevo.";

                // Puedes redirigir a la vista de Pantalla3 incluso en caso de error si es necesario
                return RedirectToAction("Pantalla3", viewModel);
            }
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using SistemaMerck.Models;
using System.Diagnostics;
using SistemaMerck.Helpers;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocacionService _locacionService;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, LocacionService locacionService)
        {
            _logger = logger;
            _configuration = configuration;
            _locacionService = locacionService;
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
                return View("Index", viewModel); // Devuelve la vista con los errores de validación
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


        private double CalcularReservaOvarica(UsuarioDto usuarioDto)
        {
            
            return (usuarioDto.EdadActual + usuarioDto.EdadPrimeraMentruacion) / 2.0;
        }

        [HttpGet]
        public IActionResult Locaciones()
        {
            // Ruta del archivo CSV
            var csvFilePath = "https://raw.githubusercontent.com/adrianparedes477/Archivo/main/clinicas.csv";

            // Obtener locaciones desde el CSV
            var locaciones = _locacionService.ObtenerLocacionesDesdeCSV(csvFilePath);

            // Convertir locaciones a LocacionDto
            var locacionesDto = _locacionService.ConvertirLocacionesALocacionDto(locaciones);

            // Pasa las locacionesDto a la vista
            return View(locacionesDto);
        }



        [HttpGet]
        public IActionResult OtraAccionLocaciones()
        {
            return View();
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
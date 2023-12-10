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
        public IActionResult ObtenerLocacionesJson()
        {
            var csvFilePath = "https://raw.githubusercontent.com/adrianparedes477/Archivo/main/clinicas.csv";
            var locaciones = _locacionService.ObtenerLocacionesDesdeCSV(csvFilePath);
            var locacionesDto = _locacionService.ConvertirLocacionesALocacionDto(locaciones);
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
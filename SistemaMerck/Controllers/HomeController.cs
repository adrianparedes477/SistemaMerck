using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using SistemaMerck.Models;
using System.Diagnostics;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
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
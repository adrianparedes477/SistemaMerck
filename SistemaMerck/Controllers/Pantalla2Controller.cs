using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;

namespace SistemaMerck.Controllers
{
    public class Pantalla2Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Pantalla2Controller(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Pantalla2(UsuarioVM viewModel)
        {
            if (viewModel.EdadPrimeraMentruacion > viewModel.EdadActual)
            {
                ModelState.AddModelError("EdadPrimeraMentruacion", "La Edad Primera Menstruación no puede ser mayor a la Edad Actual.");
                viewModel = InicializarModeloConEdades(viewModel);
                return View("~/Views/Home/Index.cshtml", viewModel);

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
        public IActionResult Pantalla2()
        {
            return RedirectToAction("Index");
        }

        private UsuarioVM InicializarModeloConEdades(UsuarioVM viewModel)
        {
            viewModel.EdadesActuales = Enumerable.Range(8, 43).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            viewModel.EdadesPrimeraMentruacion = Enumerable.Range(8, 8).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList();
            return viewModel;
        }


        private double CalcularReservaOvarica(UsuarioDto usuarioDto)
        {
            return (usuarioDto.EdadActual + usuarioDto.EdadPrimeraMentruacion) / 2.0;
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Helpers.Interface;
using SistemaMerck.Models;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using System.Diagnostics;
using SistemaMerck.Helpers;
using Microsoft.EntityFrameworkCore;
using SistemaMerck.Modelos;
using SistemaMerck.AccesoDatos.Data;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly LocacionService _locacionService;
        private readonly ICorreoService _correoService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly MerckContext _dbContext;

        public HomeController(
            LocacionService locacionService,
            ICorreoService correoService,
            IConfiguration configuration,
            ILogger<HomeController> logger,
            MerckContext dbContext)
        {
            _locacionService = locacionService;
            _correoService = correoService;
            _configuration = configuration;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.MapsKey = _configuration.GetSection("MicrosoftMaps:ApiKey").Value;
            var modelo = new UsuarioVM
            {
                Edades = Enumerable.Range(8, 43).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList()
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
        public IActionResult Pantalla2()
        {
            ViewData["NavbarColor"] = "#c33b80";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Pantalla3()
        {
            var viewModel = new UsuarioVM();
            return View();
        }

        [HttpGet]
        public IActionResult Pantalla4()
        {
            var pantalla4VM = new Pantalla4VM
            {
                Paises = GetSelectListItems(_dbContext.Paises, p => p.Id.ToString(), p => p.Nombre),
                Provincias = GetSelectListItems(_dbContext.Provincias, p => p.Id.ToString(), p => p.Provincia),
                Localidades = GetSelectListItems(_dbContext.Localidades, l => l.Id.ToString(), l => l.Localidad),
                TipoConsultas = GetSelectListItems(_dbContext.TipoConsulta, tc => tc.Id.ToString(), tc => tc.Consulta),
                Clinicas = _locacionService.ObtenerLocaciones()
            };

            return View(pantalla4VM);
        }

        private List<SelectListItem> GetSelectListItems<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = textSelector(item)
            }).ToList();
        }



        [HttpPost]
        public IActionResult ObtenerLocacionesFiltradas(Pantalla4VM viewModel)
        {
            if (viewModel.Provincia == null)
            {
                return Json(new List<ClinicasDto>());
            }

            // Obtener locaciones filtradas basadas en la provincia seleccionada
            var locacionesFiltradas = _locacionService.ObtenerLocaciones()
                .Where(locacion => locacion.Provincia == viewModel.Provincia)
                .ToList();

            return Json(locacionesFiltradas);
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

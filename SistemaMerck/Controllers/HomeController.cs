using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Models;
using System.Diagnostics;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioBusiness _usuarioService;

        public HomeController(
            ILogger<HomeController> logger, IUsuarioBusiness usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public IActionResult Bienvenida()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Index()
        {
            var modelo = _usuarioService.ObtenerDatosUsuario();
            return View(modelo);
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

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
            try
            {
                var modelo = _usuarioService.ObtenerDatosUsuario();
                return View(modelo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener datos de usuario: {ex.Message}");
                throw; // Esto permitirá que la excepción se propague para que puedas ver más detalles en la consola.
            }
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

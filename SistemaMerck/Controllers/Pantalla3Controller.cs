using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class Pantalla3Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioBusiness _usuarioService;
        public Pantalla3Controller(ILogger<HomeController> logger, IUsuarioBusiness usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Pantalla3()
        {
            var viewModel = _usuarioService.ObtenerDatosPantalla3();
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;

namespace SistemaMerck.Controllers
{
    public class Pantalla3Controller : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public Pantalla3Controller(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Pantalla3()
        {
            var viewModel = new UsuarioVM();
            return View();
        }
    }
}

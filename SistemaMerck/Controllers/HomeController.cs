using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Models;
using SistemaMerck.Modelos.ViewModels;
using System.Diagnostics;

namespace SistemaMerck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Bienvenida()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Index()
        {

            var modelo = new UsuarioVM
            {
                EdadesActuales = Enumerable.Range(8, 43).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList(),
                EdadesPrimeraMentruacion = Enumerable.Range(8, 8).Select(x => new SelectListItem { Value = x.ToString(), Text = x.ToString() }).ToList()
            };
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

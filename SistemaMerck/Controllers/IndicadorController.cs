using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class IndicadorController : Controller
    {
        private readonly ILogger<IndicadorController> _logger;
        private readonly IUsuarioBusiness _usuarioService;
        public IndicadorController(ILogger<IndicadorController> logger, IUsuarioBusiness usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Indicador()
        {
            var viewModel = _usuarioService.ObtenerDatosPantalla3();
            return View();
        }
    }
}

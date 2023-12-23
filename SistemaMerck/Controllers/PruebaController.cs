using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Models;
using System.Diagnostics;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class PruebaController : Controller
    {
        private readonly ILogger<PruebaController> _logger;
        private readonly IUsuarioBusiness _usuarioService;

        public PruebaController(
            ILogger<PruebaController> logger, IUsuarioBusiness usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

       
        [HttpGet]
        public IActionResult Prueba()
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
    }
}

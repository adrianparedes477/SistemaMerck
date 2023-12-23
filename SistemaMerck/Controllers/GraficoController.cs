﻿using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Controllers
{
    public class GraficoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioBusiness _usuarioService;
        public GraficoController(ILogger<HomeController> logger, IUsuarioBusiness usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Grafico(UsuarioVM viewModel)
        {
            if (!_usuarioService.ValidarDatosUsuario(viewModel))
            {
                ModelState.AddModelError("EdadPrimeraMentruacion", "La Edad Primera Menstruación no puede ser mayor a la Edad Actual.");
                viewModel = _usuarioService.ObtenerDatosUsuario();
                return View("~/Views/Prueba/Prueba.cshtml", viewModel);

            }
            var usuario = _usuarioService.ProcesarDatosUsuario(viewModel);

            return View(usuario);
        }


        [HttpGet]
        public IActionResult Grafico()
        {
            return RedirectToAction("Index");
        }

    }
}
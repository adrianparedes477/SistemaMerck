using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Helpers.Interface;
using SistemaMerck.Helpers;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using System.Linq.Expressions;
using SistemaMerck.Modelos;

namespace SistemaMerck.Controllers
{
    public class Pantalla4Controller : Controller
    {
        private readonly LocacionService _locacionService;
        private readonly ICorreoService _correoService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly MerckContext _dbContext;

        public Pantalla4Controller(
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

        public IActionResult MostrarFormulario()
        {
            var viewModel = new FormularioViewModel();

            // Obtener datos desde la base de datos
            viewModel.ListPaises = _dbContext.Paises.ToList();
            viewModel.ListProvincia = _dbContext.Provincias.ToList();
            viewModel.ListLocalidad = _dbContext.Localidades.ToList();
            viewModel.ListTiposConsulta = _dbContext.TipoConsulta.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MostrarFormulario(FormularioViewModel viewModel)
        {
            try
            {
                // Construye el cuerpo del correo
                var cuerpoCorreo = $"Datos del formulario:\n" +
                                   $"País: {viewModel.PaisSeleccionado}\n" +
                                   $"Provincia: {viewModel.ProvinciaSeleccionada}\n" +
                                   $"Localidad: {viewModel.LocalidadSeleccionada}\n" +
                                   $"Tipo de Consulta: {viewModel.TipoConsultaSeleccionado}" +
                                   $"Correo de Contacto: {viewModel.Correo}\n" +
                                   $"Locacion Seleccionada: {viewModel.LocacionSeleccionada}";


                
                var destinatario = "adrianjparedes477@gmail.com";

                // Utiliza el servicio de correo para enviar el correo
                await _correoService.EnviarCorreoAsync(destinatario, "Asunto del Correo", cuerpoCorreo);


                return RedirectToAction("ConsultaEnviada");
            }
            catch (Exception ex)
            {
                // Maneja el error de alguna manera, por ejemplo, muestra un mensaje de error
                _logger.LogError($"Error al enviar el formulario: {ex.Message}");
            }

            // Si llegamos aquí, hay un problema, así que volvemos a mostrar el formulario
            viewModel.ListPaises = _dbContext.Paises.ToList();
            viewModel.ListProvincia = _dbContext.Provincias.ToList();
            viewModel.ListLocalidad = _dbContext.Localidades.ToList();
            viewModel.ListTiposConsulta = _dbContext.TipoConsulta.ToList();

            return View(viewModel);
        }






        [HttpPost]
        public IActionResult ObtenerLocacionesFiltradas(FormularioViewModel viewModel)
        {
            if (viewModel.ProvinciaSeleccionada == null)
            {
                return Json(new List<ClinicasDto>());
            }

            // Obtener locaciones filtradas basadas en la provincia seleccionada
            var locacionesFiltradas = _locacionService.ObtenerLocaciones()
                .Where(locacion => locacion.Provincia == viewModel.Provincia)
                .ToList();

            return Json(locacionesFiltradas);
        }

        [HttpGet]
        public IActionResult ConsultaEnviada()
        {
            
            return View();
        }
    }

}

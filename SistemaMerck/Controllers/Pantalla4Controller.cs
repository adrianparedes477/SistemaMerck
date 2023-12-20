using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Helpers.Interface;
using SistemaMerck.Helpers;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;

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
    }
}

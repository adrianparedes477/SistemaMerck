using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using SistemaMerck.Negocio.Interface;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Helpers;
using SistemaMerck.Helpers.Interface;

namespace SistemaMerck.Negocio
{
    public class FormularioBusiness : IFormularioBusiness
    {
        private readonly LocacionService _locacionService;
        private readonly ICorreoService _correoService;
        private readonly MerckContext _dbContext;
        private readonly ILogger<FormularioBusiness> _logger;

        public FormularioBusiness(
            LocacionService locacionService,
            ICorreoService correoService,
            MerckContext dbContext,
            ILogger<FormularioBusiness> logger)
        {
            _locacionService = locacionService;
            _correoService = correoService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Pais> ObtenerPaises()
        {
            try
            {
                return _dbContext.Paises.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de países: {ex.Message}");
                throw; 
            }
        }

        public List<Provincias> ObtenerProvincias()
        {
            try
            {
                return _dbContext.Provincias.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de provincias: {ex.Message}");
                throw; 
            }
        }

        public List<Localidades> ObtenerLocalidades()
        {
            try
            {
                return _dbContext.Localidades.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de localidades: {ex.Message}");
                throw; 
            }
        }

        public List<TipoConsulta> ObtenerTiposConsulta()
        {
            try
            {
                return _dbContext.TipoConsulta.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de tipos de consulta: {ex.Message}");
                throw; 
            }
        }

        public async Task<bool> EnviarConsulta(FormularioViewModel viewModel)
        {
            try
            {
                var cuerpoCorreo = $"Datos del formulario:\n" +
                                   $"País: {viewModel.PaisSeleccionado}\n" +
                                   $"Provincia: {viewModel.ProvinciaSeleccionada}\n" +
                                   $"Localidad: {viewModel.LocalidadSeleccionada}\n" +
                                   $"Tipo de Consulta: {viewModel.TipoConsultaSeleccionado}" +
                                   $"Correo de Contacto: {viewModel.Correo}\n" +
                                   $"Locacion Seleccionada: {viewModel.LocacionSeleccionada}";

                var destinatario = "adrianjparedes477@gmail.com";

                await _correoService.EnviarCorreoAsync(destinatario, "Asunto del Correo", cuerpoCorreo);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar la consulta: {ex.Message}");
                return false;
            }
        }

        public List<ClinicasDto> ObtenerLocacionesFiltradas(string provincia)
        {
            try
            {
                if (provincia == null)
                {
                    return new List<ClinicasDto>();
                }

                return _locacionService.ObtenerLocaciones()
                    .Where(locacion => locacion.Provincia == provincia)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las locaciones filtradas: {ex.Message}");
                throw; 
            }
        }

        public FormularioViewModel ConfigurarFormularioViewModel(FormularioViewModel viewModel)
        {
            try
            {
                viewModel.ListPaises = ObtenerPaises();
                viewModel.ListProvincia = ObtenerProvincias();
                viewModel.ListLocalidad = ObtenerLocalidades();
                viewModel.ListTiposConsulta = ObtenerTiposConsulta();
                return viewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al configurar el ViewModel: {ex.Message}");
                throw; 
            }
        }
    }

}

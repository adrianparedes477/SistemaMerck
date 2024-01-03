using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using SistemaMerck.Negocio.Interface;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Helpers;
using SistemaMerck.Helpers.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

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

        public IEnumerable<SelectListItem> ObtenerPaises()
        {
            try
            {
                var paises = _dbContext.Paises.ToList();
                return paises.Select(p => new SelectListItem { Value = p.NombrePais, Text = p.NombrePais });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la lista de países: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<SelectListItem> ObtenerProvinciasFiltradas(string pais)
        {
            try
            {
                if (pais == null)
                {
                    return Enumerable.Empty<SelectListItem>();
                }

                var provincias = _dbContext.Provincias
                    .Where(provincia => provincia.Pais.NombrePais == pais)
                    .ToList();

                return provincias.Select(p => new SelectListItem { Value = p.NombreProvincia, Text = p.NombreProvincia });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las provincias filtradas: {ex.Message}");
                throw;
            }
        }


        public IEnumerable<SelectListItem> ObtenerLocalidadesFiltradas(string provincia)
        {
            try
            {
                if (provincia == null)
                {
                    return Enumerable.Empty<SelectListItem>();
                }

                var localidades = _dbContext.Localidades
                    .Where(localidad => localidad.Provincia.NombreProvincia == provincia)
                    .ToList();

                return localidades.Select(l => new SelectListItem { Value = l.NombreLocalidad, Text = l.NombreLocalidad });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las localidades filtradas: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<SelectListItem> ObtenerTiposConsulta()
        {
            try
            {
                var tiposConsulta = _dbContext.TipoConsulta.ToList();
                return tiposConsulta.Select(t => new SelectListItem { Value = t.Consulta, Text = t.Consulta });
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
                var datosFormulario = new DatosFormulario
                {
                    Clinica = viewModel.LocacionSeleccionada,
                    TipoConsulta = viewModel.TipoConsultaSeleccionado,
                    FechaHora = DateTime.Now
                };

                _dbContext.DatosFormularios.Add(datosFormulario);
                await _dbContext.SaveChangesAsync();

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
                viewModel.ListProvincia = ObtenerProvinciasFiltradas(viewModel.PaisSeleccionado);
                viewModel.ListLocalidad = ObtenerLocalidadesFiltradas(viewModel.ProvinciaSeleccionada);
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

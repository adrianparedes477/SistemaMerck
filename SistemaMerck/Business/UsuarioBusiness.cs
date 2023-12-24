using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio.Interface;

namespace SistemaMerck.Business
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly ILogger<UsuarioBusiness> _logger;

        public UsuarioBusiness(ILogger<UsuarioBusiness> logger)
        {
            _logger = logger;
        }

        public UsuarioVM ObtenerDatosUsuario()
        {
            try
            {
                return new UsuarioVM
                {
                    EdadesActuales = Enumerable.Range(8, 43).Select(x => new SelectListItem
                    { 
                        Value = x.ToString(),
                        Text = x.ToString()
                        
                    }).ToList(),
                    EdadesPrimeraMentruacion = Enumerable.Range(8, 8).Select(x => new SelectListItem 
                    { 
                        Value = x.ToString(),
                        Text = x.ToString()

                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de usuario");
                throw;
            }
        }

        public double CalcularReservaOvarica(UsuarioDto usuarioDto)
        {
            try
            {
                return (usuarioDto.EdadActual + usuarioDto.EdadPrimeraMentruacion) / 2.0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular reserva ovárica");
                throw;
            }
        }

        public List<SelectListItem> ObtenerEdadesActuales()
        {
            try
            {
                return Enumerable.Range(8, 43).Select(x => new SelectListItem 
                {
                    Value = x.ToString(), Text = x.ToString()
                    
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener edades actuales");
                throw;
            }
        }

        public List<SelectListItem> ObtenerEdadesPrimeraMentruacion()
        {
            try
            {
                return Enumerable.Range(8, 8).Select(x => new SelectListItem 
                { 
                    Value = x.ToString(), Text = x.ToString() 

                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener edades de primera menstruación");
                throw;
            }
        }

        public bool ValidarDatosUsuario(UsuarioVM viewModel)
        {
            try
            {
                return viewModel.EdadPrimeraMentruacion <= viewModel.EdadActual;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar datos de usuario");
                throw;
            }
        }

        public UsuarioVM ProcesarDatosUsuario(UsuarioVM viewModel)
        {
            try
            {
                var usuarioDto = new UsuarioDto
                {
                    EdadActual = viewModel.EdadActual,
                    EdadPrimeraMentruacion = viewModel.EdadPrimeraMentruacion
                };

                var reservaOvarica = CalcularReservaOvarica(usuarioDto);

                var usuario = new UsuarioVM
                {
                    EdadActual = viewModel.EdadActual,
                    EdadPrimeraMentruacion = viewModel.EdadPrimeraMentruacion,
                    ReservaOvarica = reservaOvarica
                };

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar datos de usuario");
                throw;
            }
        }

        public UsuarioVM ObtenerDatosPantalla3()
        {
            try
            {
                return new UsuarioVM();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos para pantalla 3");
                throw;
            }
        }
    }
}
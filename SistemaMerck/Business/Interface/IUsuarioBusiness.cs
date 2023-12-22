using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;

namespace SistemaMerck.Negocio.Interface
{
    public interface IUsuarioBusiness
    {
        UsuarioVM ObtenerDatosUsuario();
        double CalcularReservaOvarica(UsuarioDto usuarioDto);
        List<SelectListItem> ObtenerEdadesActuales();
        List<SelectListItem> ObtenerEdadesPrimeraMentruacion();
        bool ValidarDatosUsuario(UsuarioVM viewModel);
        UsuarioVM ProcesarDatosUsuario(UsuarioVM viewModel);
        UsuarioVM ObtenerDatosPantalla3();
    }
}
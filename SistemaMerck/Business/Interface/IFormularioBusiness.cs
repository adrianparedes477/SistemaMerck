using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaMerck.Negocio.Interface
{
    public interface IFormularioBusiness
    {
        IEnumerable<SelectListItem> ObtenerPaises();
        IEnumerable<SelectListItem> ObtenerProvinciasFiltradas(string pais);
        IEnumerable<SelectListItem> ObtenerLocalidadesFiltradas(string provincia);
        IEnumerable<SelectListItem> ObtenerTiposConsulta();
        Task<bool> EnviarConsulta(FormularioViewModel viewModel);
        List<ClinicasDto> ObtenerLocacionesFiltradas(string provincia);

        FormularioViewModel ConfigurarFormularioViewModel(FormularioViewModel viewModel);
    }

}

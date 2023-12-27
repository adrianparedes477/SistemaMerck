using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;

namespace SistemaMerck.Negocio.Interface
{
    public interface IFormularioBusiness
    {
        List<Paises> ObtenerPaises();
        List<string> ObtenerProvinciasFiltradas(string pais);
        List<string> ObtenerLocalidadesFiltradas(string provincia);
        List<TipoConsulta> ObtenerTiposConsulta();
        Task<bool> EnviarConsulta(FormularioViewModel viewModel);
        List<ClinicasDto> ObtenerLocacionesFiltradas(string provincia);

        FormularioViewModel ConfigurarFormularioViewModel(FormularioViewModel viewModel);
    }

}

using SistemaMerck.Modelos.Dto;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Modelos;

namespace SistemaMerck.Negocio.Interface
{
    public interface IFormularioBusiness
    {
        List<Pais> ObtenerPaises();
        List<Provincias> ObtenerProvincias();
        List<Localidades> ObtenerLocalidades();
        List<TipoConsulta> ObtenerTiposConsulta();
        Task<bool> EnviarConsulta(FormularioViewModel viewModel);
        List<ClinicasDto> ObtenerLocacionesFiltradas(string provincia);

        FormularioViewModel ConfigurarFormularioViewModel(FormularioViewModel viewModel);
    }

}

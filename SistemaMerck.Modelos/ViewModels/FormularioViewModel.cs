using SistemaMerck.Modelos.Dto;
using System.ComponentModel.DataAnnotations;

namespace SistemaMerck.Modelos.ViewModels
{
    public class FormularioViewModel
    {
        public IEnumerable<Paises> ListPaises { get; set; }
        public IEnumerable<Provincia> ListProvincia { get; set; }
        public IEnumerable<Localidades> ListLocalidad { get; set; }
        public IEnumerable<TipoConsulta> ListTiposConsulta { get; set; }

        public IEnumerable<ClinicasDto> LocacionesFiltradas { get; }

        [Required(ErrorMessage = "El campo País es obligatorio.")]
        public string PaisSeleccionado { get; set; }
        [Required(ErrorMessage = "El campo Provincia es obligatorio.")]
        public string ProvinciaSeleccionada { get; set; }
        [Required(ErrorMessage = "El campo Localidad es obligatorio.")]
        public string LocalidadSeleccionada { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Consulta es obligatorio.")]
        public string TipoConsultaSeleccionado { get; set; }
        [Required(ErrorMessage = "Debe Seleccionar una Clinica.")]
        public string LocacionSeleccionada { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Por favor, introduce una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
        public FormularioViewModel()
        {
            ListPaises = new List<Paises>();
            ListProvincia = new List<Provincia>();
            ListLocalidad = new List<Localidades>();
            ListTiposConsulta = new List<TipoConsulta>();
            LocacionesFiltradas = new List<ClinicasDto>();

            PaisSeleccionado = string.Empty;
            ProvinciaSeleccionada = string.Empty;
            LocalidadSeleccionada = string.Empty;
            TipoConsultaSeleccionado = string.Empty;
            Correo = string.Empty;
            LocacionSeleccionada = string.Empty;

            
        }
    }
}

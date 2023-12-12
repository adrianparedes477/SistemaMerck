using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.ViewModels
{
    public class UsuarioVM
    {
        public string Nombre { get; set; }

        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo 'AsuntoDelCorreo' es obligatorio.")]
        public string AsuntoDelCorreo { get; set; }

        [Required(ErrorMessage = "El campo 'CuerpoDelCorreo' es obligatorio.")]
        public string CuerpoDelCorreo { get; set; }

        public bool EsExitoso { get; set; }

        [Display(Name = "Edad Actual")]
        [Required(ErrorMessage = "Por favor, selecciona la Edad Actual.")]
        public int EdadActual { get; set; }

        [Display(Name = "Edad Primera Menstruación")]
        [Required(ErrorMessage = "Por favor, selecciona la Edad de la Primera Menstruación.")]
        [Range(typeof(int), "8", "100", ErrorMessage = "La Edad Primera Menstruación no puede ser mayor que la Edad Actual.")]
        public int EdadPrimeraMentruacion { get; set; }

        public double ReservaOvarica { get; set; }

        public List<SelectListItem> Edades { get; set; }

        public List<LocacionDto> Locaciones { get; set; }
    }

}

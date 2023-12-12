using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.Dto
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        [Display(Name = "Edad Actual")]
        public int EdadActual { get; set; }
        [Display(Name = "Edad Primera Menstruación")]
        [Range(typeof(int), "0", "100", ErrorMessage = "La Edad Primera Menstruación no puede ser mayor que la Edad Actual.")]
        public int EdadPrimeraMentruacion { get; set; }
        public double ReservaOvarica { get; set; }
    }
}

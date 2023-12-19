using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMerck.Modelos
{
    [NotMapped]
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Correo { get; set; }

        [Display(Name = "Edad Actual")]
        [Required(ErrorMessage = "El campo Edad Actual es obligatorio.")]
        public int EdadActual { get; set; }

        [Display(Name = "Edad Primera Menstruación")]
        [Required(ErrorMessage = "El campo Edad Primera Menstruación es obligatorio.")]
        [Range(0, 100, ErrorMessage = "La Edad Primera Menstruación debe estar entre 0 y 100.")]
        public int EdadPrimeraMentruacion { get; set; }

        public double ReservaOvarica { get; set; }
    }

}

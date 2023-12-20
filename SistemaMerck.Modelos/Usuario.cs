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

        [Required(ErrorMessage = "La Edad Actual es obligatoria.")]
        [Range(8, 50, ErrorMessage = "La Edad Actual debe estar entre 8 y 50.")]
        public int EdadActual { get; set; }

        [Required(ErrorMessage = "La Edad Primera Menstruación es obligatoria.")]
        [Range(8, 15, ErrorMessage = "La Edad Primera Menstruación debe estar entre 8 y 15.")]
        public int EdadPrimeraMentruacion { get; set; }

        public double ReservaOvarica { get; set; }
    }

}

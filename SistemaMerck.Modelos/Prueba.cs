using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaMerck.Modelos
{
    [NotMapped]
    public class Prueba
    {
        [Required(ErrorMessage = "La Edad Actual es obligatoria.")]
        [Range(8, 50, ErrorMessage = "La Edad Actual debe estar entre 8 y 50.")]
        public int EdadActual { get; set; }

        [Required(ErrorMessage = "La Edad Primera Menstruación es obligatoria.")]
        [Range(8, 15, ErrorMessage = "La Edad Primera Menstruación debe estar entre 8 y 15.")]
        public int EdadPrimeraMentruacion { get; set; }

        public double ReservaOvarica { get; set; }
    }

}

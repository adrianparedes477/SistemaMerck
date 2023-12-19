using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos
{
    [NotMapped]
    public class Clinicas
    {

        public string Nombre { get; set; }

        public string Provincia { get; set; }
    }
}

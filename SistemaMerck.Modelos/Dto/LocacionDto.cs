using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.Dto
{
    public class LocacionDto
    {
        [Name("nombre")]
        public string Nombre { get; set; }

        [Name("latitud")]
        public double Latitud { get; set; }

        [Name("longitud")]
        public double Longitud { get; set; }
    }
}

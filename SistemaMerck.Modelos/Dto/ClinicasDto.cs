using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.Dto
{
    public class ClinicasDto
    {
        [Name("nombre")]
        public string Nombre { get; set; }

        [Name("provincia")]
        public string Provincia { get; set; }
    }

}

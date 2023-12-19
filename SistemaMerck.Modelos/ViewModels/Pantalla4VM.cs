using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.ViewModels
{
    public class Pantalla4VM
    {
        public string Correo { get; set; }

        public string Pais { get; set; }

        public string Localidad { get; set; }

        public string TipoConsulta { get; set; }

        public string Provincia { get; set; }

        public string LocacionSeleccionada { get; set; }

        public List<ClinicasDto> LocacionesFiltradas { get; set; }

        public List<SelectListItem> Paises { get; set; }
        public List<SelectListItem> Provincias { get; set; }

        public List<SelectListItem> Localidades { get; set; }

        public List<SelectListItem> TipoConsultas { get; set; }

        public List<ClinicasDto> Clinicas { get; set; }


    }


}

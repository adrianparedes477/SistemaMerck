using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.ViewModels
{
    public class FormularioViewModel
    {
        public IEnumerable<Pais> ListPaises { get; set; }
        public IEnumerable<Provincias> ListProvincia { get; set; }
        public IEnumerable<Localidades> ListLocalidad { get; set; }
        public IEnumerable<TipoConsulta> ListTiposConsulta { get; set; }

        public List<ClinicasDto> LocacionesFiltradas { get; set; }

        public List<ClinicasDto> Clinicas { get; set; }
        public string PaisSeleccionado { get; set; }
        public string ProvinciaSeleccionada { get; set; }
        public string LocalidadSeleccionada { get; set; }
        public string TipoConsultaSeleccionado { get; set; }

        public string Provincia { get; set; }
        public string LocacionSeleccionada { get; set; }
        public string Correo { get; set; }
        public FormularioViewModel()
        {
            ListPaises = new List<Pais>();
            ListProvincia = new List<Provincias>();
            ListLocalidad = new List<Localidades>();
            ListTiposConsulta = new List<TipoConsulta>();

            PaisSeleccionado = string.Empty;
            ProvinciaSeleccionada = string.Empty;
            LocalidadSeleccionada = string.Empty;
            TipoConsultaSeleccionado = string.Empty;
            Correo = string.Empty;
            LocacionSeleccionada = string.Empty;
        }
    }





}

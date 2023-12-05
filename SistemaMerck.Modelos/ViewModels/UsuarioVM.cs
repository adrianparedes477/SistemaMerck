using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.ViewModels
{
    public class UsuarioVM
    {
        public int EdadActual { get; set; }
        public int EdadPrimeraMentruacion { get; set; }
        public double ReservaOvarica { get; set; }
        public List<SelectListItem> Edades { get; set; }
    }
}

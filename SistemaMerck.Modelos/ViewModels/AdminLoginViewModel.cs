using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.Modelos.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "El Nombre de Usuario es Requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El Password Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

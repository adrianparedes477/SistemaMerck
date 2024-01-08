using SistemaMerck.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.AccesoDatos.Repositorio.Interfaces
{
    public interface IUnidadTrabajo : IDisposable 
    {

        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }
        Task Guardar();
    }
}

using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.AccesoDatos.Repositorio.Interfaces
{
    public interface ILocacionRepository
    {
        List<LocacionDto> ObtenerLocaciones();
    }
}

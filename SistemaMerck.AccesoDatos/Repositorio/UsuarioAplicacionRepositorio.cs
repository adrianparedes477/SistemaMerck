using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using SistemaMerck.Modelos;

namespace SistemaMerck.AccesoDatos.Repositorio
{
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {

        private readonly MerckContext _db;

        public UsuarioAplicacionRepositorio(MerckContext db) : base(db)
        {
            _db = db;
        }


    }
}

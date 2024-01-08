using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;

namespace SistemaMerck.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly MerckContext _db;
       
        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }

       
        public UnidadTrabajo(MerckContext db)
        {
            _db = db;
            
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
           
        }
      
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}

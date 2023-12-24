using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using SistemaMerck.Modelos.Dto;

namespace SistemaMerck.AccesoDatos.Repositorio
{
    public class BaseDatosLocacionRepository 
    {
        private readonly MerckContext _dbContext; 

        public BaseDatosLocacionRepository(MerckContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public List<LocacionDto> ObtenerLocaciones()
        //{
        //    //// Lógica para obtener locaciones desde la base de datos
        //    //var locacionesDesdeBaseDatos = _dbContext.Locaciones.Select(locacion => new LocacionDto
        //    //{
        //    //    Nombre = locacion.Nombre,
        //    //    Provincia = locacion.Provincia
        //    //}).ToList();

        //    //return locacionesDesdeBaseDatos;
        //}
    }
}

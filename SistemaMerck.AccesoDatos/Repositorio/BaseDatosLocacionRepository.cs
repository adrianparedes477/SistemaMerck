using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.AccesoDatos.Repositorio
{
    public class BaseDatosLocacionRepository : ILocacionRepository
    {
        private readonly ApplicationDbContext _dbContext; 

        public BaseDatosLocacionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LocacionDto> ObtenerLocaciones()
        {
            // Lógica para obtener locaciones desde la base de datos
            var locacionesDesdeBaseDatos = _dbContext.Locaciones.Select(locacion => new LocacionDto
            {
                Nombre = locacion.Nombre,
                Latitud = locacion.Latitud,
                Longitud = locacion.Longitud
            }).ToList();

            return locacionesDesdeBaseDatos;
        }
    }
}

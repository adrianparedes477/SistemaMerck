using Newtonsoft.Json;
using SistemaMerck.Modelos;
using System.Globalization;
using CsvHelper;
using SistemaMerck.Modelos.Dto;
using CsvHelper.Configuration;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;

namespace SistemaMerck.Helpers
{
    public class LocacionService
    {
        private readonly ILocacionRepository _locacionRepository;

        public LocacionService(ILocacionRepository locacionRepository)
        {
            _locacionRepository = locacionRepository;
        }

        public List<LocacionDto> ObtenerLocaciones()
        {
            var locaciones = _locacionRepository.ObtenerLocaciones();
            return ConvertirLocacionesALocacionDto(locaciones);
        }

        public List<LocacionDto> ConvertirLocacionesALocacionDto(List<LocacionDto> locaciones)
        {
            // Mapear las Locaciones a LocacionDto directamente
            var locacionesDto = locaciones.Select(locacion => new LocacionDto
            {
                Nombre = locacion.Nombre,
                Latitud = locacion.Latitud,
                Longitud = locacion.Longitud
            }).ToList();

            return locacionesDto;
        }
    }

}

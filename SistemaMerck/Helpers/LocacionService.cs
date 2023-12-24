using Newtonsoft.Json;
using SistemaMerck.Modelos;
using System.Globalization;
using CsvHelper;
using SistemaMerck.Modelos.Dto;
using CsvHelper.Configuration;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SistemaMerck.Helpers
{
    public class LocacionService
    {
        private readonly ILocacionRepository _locacionRepository;

        public LocacionService(ILocacionRepository locacionRepository)
        {
            _locacionRepository = locacionRepository;
        }

        public List<ClinicasDto> ObtenerLocaciones()
        {
            var locaciones = _locacionRepository.ObtenerLocaciones();
            return ConvertirLocacionesALocacionDto(locaciones);
        }

        
        public List<ClinicasDto> ConvertirLocacionesALocacionDto(List<ClinicasDto> locaciones)
        {
            // Mapear las Locaciones a LocacionDto directamente
            var locacionesDto = locaciones.Select(locacion => new ClinicasDto
            {
                Nombre = locacion.Nombre,
                Provincia = locacion.Provincia,
                
            }).ToList();

            return locacionesDto;
        }
    }

}

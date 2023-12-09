using Newtonsoft.Json;
using SistemaMerck.Modelos;
using System.Globalization;
using CsvHelper;
using SistemaMerck.Modelos.Dto;
using CsvHelper.Configuration;

namespace SistemaMerck.Helpers
{
    public class LocacionService
    {
        public List<LocacionDto> ObtenerLocacionesDesdeCSV(string filePath)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var csvContent = httpClient.GetStringAsync(filePath).Result;

                    using (var reader = new StringReader(csvContent))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                    }))
                    {
                        // Mapea las filas a LocacionDto en lugar de Locacion
                        return csv.GetRecords<LocacionDto>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                throw new Exception($"Error al obtener locaciones desde el archivo CSV. Detalles: {ex.Message}", ex);
            }
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

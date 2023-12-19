using CsvHelper.Configuration;
using CsvHelper;
using SistemaMerck.AccesoDatos.Repositorio.Interfaces;
using SistemaMerck.Modelos.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMerck.AccesoDatos.Repositorio
{
    public class ArchivoLocacionRepository : ILocacionRepository
    {
        private readonly string _filePath;

        public ArchivoLocacionRepository(string filePath)
        {
            _filePath = filePath;
        }
        public List<ClinicasDto> ObtenerLocaciones()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var csvContent = httpClient.GetStringAsync(_filePath).Result;

                    using (var reader = new StringReader(csvContent))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                    }))
                    {
                        // Mapea las filas a LocacionDto en lugar de Locacion
                        return csv.GetRecords<ClinicasDto>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener locaciones desde el archivo CSV. Detalles: {ex.Message}", ex);
            }
        }
    }
}

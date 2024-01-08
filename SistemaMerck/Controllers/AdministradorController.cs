using Microsoft.AspNetCore.Mvc;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Modelos.ViewModels;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using SistemaMerck.Modelos;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SistemaMerck.Utilidades;

namespace SistemaMerck.Controllers
{
    [Authorize(Roles = DS.Role_Admin)]
    public class AdministradorController : Controller
    {
        private readonly MerckContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdministradorController(MerckContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Dashboard()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var viewModel = new DashboardViewModel
                {
                    DatosFormularioList = _dbContext.DatosFormularios.ToList()
                };

                return View(viewModel);
            }

            // Si llegas aquí, la autenticación no es válida
            return RedirectToAction("AccessDenied", "Account", new { area = "Identity" });
        }




        [HttpPost]
        public IActionResult FiltrarDashboard(string fechaInicio, string fechaFin)
        {
            var username = HttpContext.Session.GetString("UserName");

            if (_signInManager.IsSignedIn(User))
            {
                DateTime? fechaInicioParsed = !string.IsNullOrEmpty(fechaInicio) ? DateTime.Parse(fechaInicio) : (DateTime?)null;
                DateTime? fechaFinParsed = !string.IsNullOrEmpty(fechaFin) ? DateTime.Parse(fechaFin) : (DateTime?)null;

                var query = _dbContext.DatosFormularios.AsQueryable();

                if (fechaInicioParsed.HasValue)
                {
                    query = query.Where(d => d.FechaHora >= fechaInicioParsed);
                }

                if (fechaFinParsed.HasValue)
                {
                    query = query.Where(d => d.FechaHora <= fechaFinParsed);
                }

                var datosFiltrados = query.ToList();

                // Serializar la lista a JSON
                var json = JsonSerializer.Serialize(datosFiltrados);
                var jsonBytes = Encoding.UTF8.GetBytes(json);

                // Guardar los datos en la sesión
                HttpContext.Session.Set("DatosFiltrados", jsonBytes);

                return Json(new { success = true });
            }

            // Si llegas aquí, la autenticación no es válida
            return Json(new { success = false });
        }



        [HttpGet]
        public IActionResult ExportarExcel()
        {
            // Obtener los datos de la sesión
            var jsonBytes = HttpContext.Session.Get("DatosFiltrados");

            // Deserializar los datos
            var json = Encoding.UTF8.GetString(jsonBytes);
            var datosFiltrados = JsonSerializer.Deserialize<List<DatosFormulario>>(json);

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("DatosFormulario");

            // Encabezados
            var headers = new string[] { "Clinica", "Tipo de Consulta", "Fecha y Hora", "Url" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Datos
            var rowIndex = 2;
            foreach (var formulario in datosFiltrados)
            {
                worksheet.Cell(rowIndex, 1).Value = formulario.Clinica;
                worksheet.Cell(rowIndex, 2).Value = formulario.TipoConsulta;
                worksheet.Cell(rowIndex, 3).Value = formulario.FechaHora.ToString();
                worksheet.Cell(rowIndex, 4).Value = formulario.Url;
                rowIndex++;
            }

            // Guardar el archivo Excel
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DatosFormulario.xlsx");
        }



        [HttpGet]
        public IActionResult ExportarPdf()
        {
            // Obtener los datos de la sesión
            var jsonBytes = HttpContext.Session.Get("DatosFiltrados");

            // Deserializar los datos
            var json = Encoding.UTF8.GetString(jsonBytes);
            var datosFiltrados = JsonSerializer.Deserialize<List<DatosFormulario>>(json);

            // Crear una instancia de DashboardViewModel y establecer los datos filtrados
            var viewModel = new DashboardViewModel
            {
                DatosFormularioList = datosFiltrados
            };

            // Pasar el modelo al método ViewAsPdf
            var pdf = new ViewAsPdf(viewModel)
            {
                FileName = "ExportarPdf.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };

            return pdf;
        }


    }
}

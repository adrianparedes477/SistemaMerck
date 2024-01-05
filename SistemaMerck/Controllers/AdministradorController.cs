using Microsoft.AspNetCore.Mvc;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Modelos.ViewModels;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using SistemaMerck.Modelos;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace SistemaMerck.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly MerckContext _dbContext;

        public AdministradorController(MerckContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Intenta autenticar sin hash y salting
                var administrador = _dbContext.Administradores
                    .FirstOrDefault(a => a.UserName == model.UserName && a.Password == model.Password);

                if (administrador != null)
                {
                    // El inicio de sesión es exitoso, puedes redirigir a una página de administrador.
                    HttpContext.Session.SetString("UserName", model.UserName);

                    return RedirectToAction("Dashboard");
                }

                // Si llegas aquí, el inicio de sesión falló
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
            }

            return View(model);
        }

        public IActionResult Dashboard()
        {
            var username = HttpContext.Session.GetString("UserName");

            if (!string.IsNullOrEmpty(username))
            {
                var viewModel = new DashboardViewModel
                {
                    DatosFormularioList = _dbContext.DatosFormularios.ToList()
                };

                return View(viewModel);
            }

            // Si llegas aquí, la autenticación no es válida
            return RedirectToAction("Login");
        }




        [HttpPost]
        public IActionResult FiltrarDashboard(string fechaInicio, string fechaFin)
        {
            var username = HttpContext.Session.GetString("UserName");

            if (!string.IsNullOrEmpty(username))
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

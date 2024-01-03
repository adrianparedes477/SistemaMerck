using Microsoft.AspNetCore.Mvc;
using SistemaMerck.Models;
using System.Diagnostics;
using SistemaMerck.Negocio.Interface;
using SistemaMerck.AccesoDatos.Data;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Helpers;
using ClosedXML.Excel;
using Rotativa.AspNetCore;

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
                    return RedirectToAction("Dashboard", new { username = model.UserName });
                }

                // Si llegas aquí, el inicio de sesión falló
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
            }

            return View(model);
        }

        public IActionResult Dashboard(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var viewModel = new DashboardViewModel
                {
                    UserName = username,
                    DatosFormularioList = _dbContext.DatosFormularios.ToList()
                };

                return View(viewModel);
            }

            // Si llegas aquí, la autenticación no es válida
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            // Asegúrate de tener el modelo correcto aquí
            var viewModel = new DashboardViewModel
            {
                DatosFormularioList = _dbContext.DatosFormularios.ToList()
            };

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
            foreach (var formulario in viewModel.DatosFormularioList)
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
            // Asegúrate de tener el modelo correcto aquí
            var viewModel = new DashboardViewModel
            {
                DatosFormularioList = _dbContext.DatosFormularios.ToList()
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

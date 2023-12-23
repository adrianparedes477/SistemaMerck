using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaMerck.Controllers;
using SistemaMerck.Modelos.ViewModels;
using SistemaMerck.Negocio.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SistemaMerck.test
{
    [TestClass]
    public class Pantalla4ControllerTests
    {
        [TestMethod]
        public async Task MostrarFormulario_DeberiaRetornarVistaConFormularioViewModel()
        {
            // Arrange (Configurar)
            var formularioServiceMock = new Mock<IFormularioBusiness>();
            var loggerMock = new Mock<ILogger<HomeController>>();

            var viewModelStub = new FormularioViewModel { /* Datos simulados */ };
            formularioServiceMock.Setup(x => x.ConfigurarFormularioViewModel(It.IsAny<FormularioViewModel>())).Callback<FormularioViewModel>(vm => vm = viewModelStub);

            var controller = new FormularioController(formularioServiceMock.Object, loggerMock.Object);

            // Act (Actuar)
            var result = controller.MostrarFormulario() as ViewResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("MostrarFormulario", result.ViewName);
            Assert.AreSame(viewModelStub, result.Model);
        }

        [TestMethod]
        public async Task MostrarFormulario_CuandoEnvioConsultaExitosamente_DeberiaRedirigirAConsultaEnviada()
        {
            // Arrange (Configurar)
            var formularioServiceMock = new Mock<IFormularioBusiness>();
            var loggerMock = new Mock<ILogger<HomeController>>();

            var viewModelStub = new FormularioViewModel { /* Datos simulados */ };
            formularioServiceMock.Setup(x => x.EnviarConsulta(It.IsAny<FormularioViewModel>())).ReturnsAsync(true);

            var controller = new FormularioController(formularioServiceMock.Object, loggerMock.Object);

            // Act (Actuar)
            var result = await controller.MostrarFormulario(viewModelStub) as RedirectToActionResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("ConsultaEnviada", result.ActionName);
        }

        // Agrega más pruebas según sea necesario para cubrir otros escenarios del controlador.
    }
}

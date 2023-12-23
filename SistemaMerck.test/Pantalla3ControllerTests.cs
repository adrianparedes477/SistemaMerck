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
    public class Pantalla3ControllerTests
    {
        [TestMethod]
        public void Pantalla3_DeberiaRetornarVistaConDatosPantalla3()
        {
            // Arrange (Configurar)
            var loggerMock = new Mock<ILogger<HomeController>>();
            var usuarioServiceMock = new Mock<IUsuarioBusiness>();

            var datosPantalla3Stub = new UsuarioVM { /* Datos simulados */ };
            usuarioServiceMock.Setup(x => x.ObtenerDatosPantalla3()).Returns(datosPantalla3Stub);

            var controller = new IndicadorController(loggerMock.Object, usuarioServiceMock.Object);

            // Act (Actuar)
            var result = controller.Pantalla3() as ViewResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("Pantalla3", result.ViewName);
            Assert.AreSame(datosPantalla3Stub, result.Model);
        }
    }
}

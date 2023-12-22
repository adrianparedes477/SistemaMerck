using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaMerck.Controllers;
using SistemaMerck.Models;
using SistemaMerck.Negocio.Interface;
using SistemaMerck.Modelos.ViewModels;

namespace SistemaMerck.test
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_DeberiaRetornarVistaConDatosUsuario()
        {
            // Arrange (Configurar)
            var loggerMock = new Mock<ILogger<HomeController>>();
            var usuarioServiceMock = new Mock<IUsuarioBusiness>();

            var datosUsuarioStub = new UsuarioVM { /* Datos simulados */ };
            usuarioServiceMock.Setup(x => x.ObtenerDatosUsuario()).Returns(datosUsuarioStub);

            var controller = new HomeController(loggerMock.Object, usuarioServiceMock.Object);

            // Act (Actuar)
            var result = controller.Index() as ViewResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreSame(datosUsuarioStub, result.Model);
        }
    }
}

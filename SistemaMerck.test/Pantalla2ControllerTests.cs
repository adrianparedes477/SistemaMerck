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
    public class Pantalla2ControllerTests
    {
        [TestMethod]
        public void Pantalla2_DeberiaRetornarVistaValidacionFallida()
        {
            // Arrange (Configurar)
            var loggerMock = new Mock<ILogger<HomeController>>();
            var usuarioServiceMock = new Mock<IUsuarioBusiness>();

            var datosUsuarioStub = new UsuarioVM { /* Datos simulados */ };
            usuarioServiceMock.Setup(x => x.ObtenerDatosUsuario()).Returns(datosUsuarioStub);
            usuarioServiceMock.Setup(x => x.ValidarDatosUsuario(It.IsAny<UsuarioVM>())).Returns(false);

            var controller = new GraficoController(loggerMock.Object, usuarioServiceMock.Object);

            // Act (Actuar)
            var result = controller.Pantalla2(new UsuarioVM()) as ViewResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("~/Views/Home/Index.cshtml", result.ViewName);
            Assert.AreSame(datosUsuarioStub, result.Model);
        }

        [TestMethod]
        public void Pantalla2_DeberiaRetornarVistaConDatosProcesados()
        {
            // Arrange (Configurar)
            var loggerMock = new Mock<ILogger<HomeController>>();
            var usuarioServiceMock = new Mock<IUsuarioBusiness>();

            var datosUsuarioStub = new UsuarioVM { /* Datos simulados */ };
            usuarioServiceMock.Setup(x => x.ObtenerDatosUsuario()).Returns(datosUsuarioStub);
            usuarioServiceMock.Setup(x => x.ValidarDatosUsuario(It.IsAny<UsuarioVM>())).Returns(true);
            usuarioServiceMock.Setup(x => x.ProcesarDatosUsuario(It.IsAny<UsuarioVM>())).Returns(new UsuarioVM());

            var controller = new GraficoController(loggerMock.Object, usuarioServiceMock.Object);

            // Act (Actuar)
            var result = controller.Pantalla2(new UsuarioVM()) as ViewResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("Pantalla2", result.ViewName);
            Assert.IsNotNull(result.Model); // Agrega más validaciones según tus necesidades
        }

        [TestMethod]
        public void Pantalla2_DeberiaRedirigirAIndexEnHttpGet()
        {
            // Arrange (Configurar)
            var loggerMock = new Mock<ILogger<HomeController>>();
            var usuarioServiceMock = new Mock<IUsuarioBusiness>();

            var controller = new GraficoController(loggerMock.Object, usuarioServiceMock.Object);

            // Act (Actuar)
            var result = controller.Pantalla2() as RedirectToActionResult;

            // Assert (Afirmar)
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}

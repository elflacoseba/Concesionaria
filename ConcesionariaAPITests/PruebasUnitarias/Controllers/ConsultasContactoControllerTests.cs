using Concesionaria.API.Controllers;
using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Concesionaria.API.Tests.Controllers
{
    [TestClass]
    public class ConsultasContactoControllerTests
    {
        private Mock<IConsultaContactoService>? _serviceMock;
        private ConsultasContactoController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IConsultaContactoService>();
            _controller = new ConsultasContactoController(_serviceMock.Object);
        }

        [TestMethod]
        public async Task GetPaged_ReturnsOkResult_WithPagedData()
        {
            // Arrange
            var pagedResult = new PagedResultDTO<ConsultaContactoDTO>
            {
                Items = new List<ConsultaContactoDTO>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };
            _serviceMock!.Setup(s => s.GetConsultasContactoPagedAsync(1, 10)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller!.GetPaged(1, 10);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(pagedResult, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsConsultaContacto_WhenFound()
        {
            // Arrange
            var consulta = new ConsultaContactoDTO
            {
                Id = 1,
                Nombre = "Test",
                Apellido = "Apellido",
                Email = "test@mail.com",
                Telefono = "123",
                Mensaje = "Mensaje",
                FechaEnvio = DateTime.Now,
                NoLeida = true
            };
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync(consulta);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.AreEqual(consulta, result.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync((ConsultaContactoDTO)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Post_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act
            var result = await _controller.Post(null);

            // Assert
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual("El objeto ConsultaContactoCreacionDTO no puede ser nulo.", badRequest.Value);
        }

        [TestMethod]
        public async Task Post_ReturnsCreatedAtRoute_WhenValid()
        {
            // Arrange
            var creacionDto = new ConsultaContactoCreacionDTO
            {
                Nombre = "Test",
                Apellido = "Apellido",
                Email = "test@mail.com",
                Telefono = "123",
                Mensaje = "Mensaje"
            };
            var consulta = new ConsultaContactoDTO
            {
                Id = 1,
                Nombre = "Test",
                Apellido = "Apellido",
                Email = "test@mail.com",
                Telefono = "123",
                Mensaje = "Mensaje",
                FechaEnvio = DateTime.Now,
                NoLeida = true
            };
            _serviceMock.Setup(s => s.CreateConsultaContactoAsync(creacionDto)).ReturnsAsync(consulta);

            // Act
            var result = await _controller.Post(creacionDto);

            // Assert
            var createdResult = result as CreatedAtRouteResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("ObtenerConsultaContacto", createdResult.RouteName);
            Assert.AreEqual(consulta, createdResult.Value);
        }

        [TestMethod]
        public async Task Put_ReturnsNotFound_WhenConsultaNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync((ConsultaContactoDTO)null);

            // Act
            var result = await _controller.Put(1, new ConsultaContactoActualizacionDTO());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Put_ReturnsNoContent_WhenUpdated()
        {
            // Arrange
            var consulta = new ConsultaContactoDTO
            {
                Id = 1,
                Nombre = "Test",
                Apellido = "Apellido",
                Email = "test@mail.com",
                Telefono = "123",
                Mensaje = "Mensaje",
                FechaEnvio = DateTime.Now,
                NoLeida = true
            };
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync(consulta);
            _serviceMock.Setup(s => s.UpdateConsultaContactoAsync(1, It.IsAny<ConsultaContactoActualizacionDTO>())).ReturnsAsync(1);

            // Act
            var result = await _controller.Put(1, new ConsultaContactoActualizacionDTO());

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_WhenConsultaNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync((ConsultaContactoDTO)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var consulta = new ConsultaContactoDTO
            {
                Id = 1,
                Nombre = "Test",
                Apellido = "Apellido",
                Email = "test@mail.com",
                Telefono = "123",
                Mensaje = "Mensaje",
                FechaEnvio = DateTime.Now,
                NoLeida = true
            };
            _serviceMock.Setup(s => s.GetConsultaContactoByIdAsync(1)).ReturnsAsync(consulta);
            _serviceMock.Setup(s => s.DeleteConsultaContactoAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
    }
}
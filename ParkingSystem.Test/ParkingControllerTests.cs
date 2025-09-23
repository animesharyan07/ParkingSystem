using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingSystem.Models;
using Moq;
using ParkingSystem.API.Controllers;
using ParkingSystem.Services;
using Xunit;

namespace Tests
{
    public class ParkingControllerTests
    {
        private readonly Mock<IParkingServices> _mockService;
        private readonly Mock<ILogger<ParkingController>> _mockLogger;
        private readonly ParkingController _controller;

        public ParkingControllerTests()
        {
            _mockService = new Mock<IParkingServices>();
            _mockLogger = new Mock<ILogger<ParkingController>>();
            _controller = new ParkingController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsOkWithParkings()
        {
            _mockService.Setup(s => s.Get()).Returns(new List<Parking>
    {
        new Parking { Id = "1", Name = "Lot A" }
    });

            var result = _controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var parkings = Assert.IsType<List<Parking>>(okResult.Value);
            Assert.Single(parkings);
            Assert.Equal("Lot A", parkings[0].Name);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenParkingNotExists()
        {
           
            _mockService.Setup(s => s.Get("99")).Returns((Parking)null);

            // Act
            var result = _controller.Delete("99");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOk_WhenParkingExists()
        {
            // Arrange
            var parking = new Parking { Id = "1", Name = "Lot A" };
            _mockService.Setup(s => s.Get("1")).Returns(parking);
            _mockService.Setup(s => s.Remove("1"));

            // Act
            var result = _controller.Delete("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("deleted", okResult.Value.ToString());
        }
    }
}

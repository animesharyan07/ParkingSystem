using Xunit;
using Moq;
using Services;
using Repository;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.Test
{
    public class ParkingServiceTests
    {
        private readonly Mock<IParkingRepository> _mockRepo;
        private readonly ParkingService _service;

        public ParkingServiceTests()
        {
            _mockRepo = new Mock<IParkingRepository>();
            _service = new ParkingService(_mockRepo.Object);
        }

        [Fact]
        public void Get_ReturnsAllParkings()
        {
            // Arrange
            var parkings = new List<Parking>
            {
                new Parking { Id = "64f1a1", Name="Lot A", Capacity=50, AvailableSpots=20, IsPaid=false },
                new Parking { Id = "64f1a2", Name="Lot B", Capacity=30, AvailableSpots=10, IsPaid=true }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(parkings);

            // Act
            var result = _service.Get();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Lot A", result[0].Name);
        }

        [Fact]
        public void GetById_ReturnsCorrectParking()
        {
            // Arrange
            var parking = new Parking { Id = "64f1a1", Name = "Lot A", Capacity = 50 };
            _mockRepo.Setup(r => r.GetByIdAsync("64f1a1")).ReturnsAsync(parking);

            // Act
            var result = _service.Get("64f1a1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Lot A", result.Name);
        }

        [Fact]
        public void Create_AddsParking()
        {
            // Arrange
            var newParking = new Parking { Name = "Lot C", Capacity = 40, AvailableSpots = 40 };
            _mockRepo.Setup(r => r.CreateAsync(newParking)).Returns(Task.CompletedTask);

            // Act
            var result = _service.Create(newParking);

            // Assert
            Assert.Equal("Lot C", result.Name);
            _mockRepo.Verify(r => r.CreateAsync(newParking), Times.Once);
        }

        [Fact]
        public void Remove_CallsRepositoryDelete()
        {
            // Arrange
            var id = "64f1a1";
            _mockRepo.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            _service.Remove(id);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}

using Xunit;
using Moq;
using Repository;
using Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Collections.Generic;
using DBSettings;
namespace ParkingSystem.Test
{
    public class ParkingRepositoryTests
    {
        [Fact]
        public void CreateAsync_CallsInsertOneAsync()
        {
            // Arrange
            var mockCollection = new Mock<IMongoCollection<Parking>>();
            var mockSettings = new Mock<IOptions<ParkingDatabaseSetting>>();
            mockSettings.Setup(s => s.Value).Returns(new ParkingDatabaseSetting
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDB"
            });

            var parking = new Parking { Name = "Lot Test" };

        }
    }
}

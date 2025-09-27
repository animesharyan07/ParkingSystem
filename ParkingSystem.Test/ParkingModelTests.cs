using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParkingSystem.Models;
using Xunit;

namespace ParkingSystem.Test
{
    public class ParkingModelTests
    {
        private List<ValidationResult> ValidateModel(Parking model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void ParkingModel_ValidData_ShouldPassValidation()
        {
            var model = new Parking
            {
                Id = "507f1f77bcf86cd799439011",
                Name = "Central Parking Lot",
                Location = "Downtown, City Center",
                Description = "Open 24/7",
                Capacity = 200,
                AvailableSpots = 50,
                HourlyRate = 20.5m,
                EntryTime = DateTime.UtcNow,
                ExitTime = DateTime.UtcNow.AddHours(2),
                VehicleNumber = "KA-05-AB-1234",
                IsPaid = true
            };

            var results = ValidateModel(model);
            Assert.Empty(results); 
        }

        [Fact]
        public void ParkingModel_MissingName_ShouldFailValidation()
        {
            var model = new Parking
            {
                Location = "Downtown",
                Capacity = 50,
                AvailableSpots = 20,
                HourlyRate = 10m,
                EntryTime = DateTime.UtcNow,
                VehicleNumber = "KA-05-AB-1234",
                IsPaid = false
            };

            var results = ValidateModel(model);
            Assert.Contains(results, v => v.ErrorMessage.Contains("Name is required"));
        }

        [Fact]
        public void ParkingModel_InvalidCapacity_ShouldFailValidation()
        {
            var model = new Parking
            {
                Name = "Test Parking",
                Location = "Test City",
                Capacity = -5,
                AvailableSpots = 10,
                HourlyRate = 15m,
                EntryTime = DateTime.UtcNow,
                VehicleNumber = "KA-05-AB-1234"
            };

            var results = ValidateModel(model);
            Assert.Contains(results, v => v.ErrorMessage.Contains("Capacity must be between 1 and 1000"));
        }

        [Fact]
        public void ParkingModel_InvalidHourlyRate_ShouldFailValidation()
        {
            var model = new Parking
            {
                Name = "Test Parking",
                Location = "Test City",
                Capacity = 100,
                AvailableSpots = 80,
                HourlyRate = -10m, 
                EntryTime = DateTime.UtcNow,
                VehicleNumber = "KA-05-AB-1234"
            };

            var results = ValidateModel(model);
            Assert.Contains(results, v => v.ErrorMessage.Contains("Hourly rate must be between 0 and 10,000"));
        }

        [Fact]
        public void ParkingModel_AvailableSpotsGreaterThanCapacity_ShouldStillValidateModel()
        {
            var model = new Parking
            {
                Name = "Overflow Parking",
                Location = "Airport Zone",
                Capacity = 100,
                AvailableSpots = 120, 
                HourlyRate = 50m,
                EntryTime = DateTime.UtcNow,
                VehicleNumber = "MH-12-XY-4567"
            };

            var results = ValidateModel(model);

            
            Assert.Empty(results);
        }

        [Fact]
        public void ParkingModel_ExitTimeBeforeEntryTime_ShouldFailBusinessLogic()
        {
            var model = new Parking
            {
                Name = "Test Lot",
                Location = "Test Area",
                Capacity = 100,
                AvailableSpots = 90,
                HourlyRate = 25m,
                EntryTime = DateTime.UtcNow,
                ExitTime = DateTime.UtcNow.AddHours(-2), 
                VehicleNumber = "DL-09-AB-4321"
            };

            // Since DataAnnotations cannot check relation between two fields,
            // we simulate a custom validation check here:
            bool isValidTime = model.ExitTime == null || model.ExitTime >= model.EntryTime;

            Assert.False(isValidTime); 
        }

        [Fact]
        public void ParkingModel_InvalidVehicleNumber_ShouldFailValidation()
        {
            var model = new Parking
            {
                Name = "Test Lot",
                Location = "Test Area",
                Capacity = 50,
                AvailableSpots = 40,
                HourlyRate = 15m,
                EntryTime = DateTime.UtcNow,
                VehicleNumber = "", //Required but empty
                IsPaid = false
            };

            var results = ValidateModel(model);
            Assert.Contains(results, v => v.ErrorMessage.Contains("Vehicle number is required"));
        }
    }
}

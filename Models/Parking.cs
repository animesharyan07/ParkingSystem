using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingSystem.Models
{
    public class Parking
    {
        [BsonId] // MongoDB _id
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters")]
        [BsonElement("location")]
        public string Location { get; set; }

        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters")]
        [BsonElement("description")]
        public string? Description { get; set; }

        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available spots cannot be negative")]
        [BsonElement("availableSpots")]
        public int AvailableSpots { get; set; }

        [Range(0.0, 10000.0, ErrorMessage = "Hourly rate must be between 0 and 10,000")]
        [BsonElement("hourlyRate")]
        public decimal HourlyRate { get; set; }

        [Required(ErrorMessage = "Entry time is required")]
        [BsonElement("entryTime")]
        public DateTime EntryTime { get; set; }

        [BsonElement("exitTime")]
        public DateTime? ExitTime { get; set; }

        [Required(ErrorMessage = "Vehicle number is required")]
        //[RegularExpression(@"^[A-Z0-9-]{5,15}$", ErrorMessage = "Invalid vehicle number format")]
        [BsonElement("vehicleNumber")]
        public string VehicleNumber { get; set; }

        [BsonElement("isPaid")]
        public bool IsPaid { get; set; }
    }
}

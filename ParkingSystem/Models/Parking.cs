using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingSystem.Models
{
    public class Parking
    {
        [BsonId] // MongoDB _id
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [BsonElement("availableSpots")]
        public int AvailableSpots { get; set; }

        [BsonElement("hourlyRate")]
        public decimal HourlyRate { get; set; }

        [BsonElement("entryTime")]
        public DateTime EntryTime { get; set; }

        [BsonElement("exitTime")]
        public DateTime? ExitTime { get; set; }

        [BsonElement("vehicleNumber")]
        public string VehicleNumber { get; set; }

        [BsonElement("isPaid")]
        public bool IsPaid { get; set; }

    }
}

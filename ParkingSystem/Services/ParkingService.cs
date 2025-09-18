using ParkingSystem.Models;
using MongoDB.Driver;

namespace ParkingSystem.Services
{
    public class ParkingService : IParkingServices
    {
        private readonly IMongoCollection<Parking> _parking;

        public ParkingService(IParkingDatabase settings, IMongoClient mongoClient) 
        {
            var database= mongoClient.GetDatabase(settings.DatabaseName);
            _parking =database.GetCollection<Parking>(settings.CollectionName); 

        }

        public Parking Create(Parking parking)
        {
            _parking.InsertOne(parking);
            return parking;
        }

        public List<Parking> Get()
        {
            return _parking.Find(parking => true).ToList();

        }

        public Parking Get(string id)
        {
            return _parking.Find(parking=>parking.Id==id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _parking.DeleteOne(parking => parking.Id == id);

        }

        public void Update(string id, Parking parkingIn)
        {
            _parking.ReplaceOne(parking => parking.Id == id, parkingIn);

        }
    }
}

using ParkingSystem.Models;
using ParkingSystem.Repository;
using System.Collections.Generic;
namespace ParkingSystem.Services
{
    public class ParkingService : IParkingServices
    {
        private readonly IParkingRepository _parkingRepository;

        public ParkingService(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public Parking Create(Parking parking)
        {
            // repository is async → blocking here for simplicity
            _parkingRepository.CreateAsync(parking).Wait();
            return parking;
        }

        public List<Parking> Get()
        {
            return _parkingRepository.GetAllAsync().Result.ToList();
        }

        public Parking? Get(string id)
        {
            return _parkingRepository.GetByIdAsync(id).Result;
        }

        public void Remove(string id)
        {
            _parkingRepository.DeleteAsync(id).Wait();
        }

        public void Update(string id, Parking parkingIn)
        {
            _parkingRepository.UpdateAsync(id, parkingIn).Wait();
        }
    }
}

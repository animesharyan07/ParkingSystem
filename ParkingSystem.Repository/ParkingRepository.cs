using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingSystem.DBSettings;
using Microsoft.Extensions.Options;
using ParkingSystem.Models;
using MongoDB.Driver;

namespace ParkingSystem.Repository
{
    public class ParkingRepository(IOptions<ParkingDatabaseSetting> settings, IMongoClient mongoClient) : Repository<Parking>(settings, mongoClient), IParkingRepository
    {
        public async Task<IEnumerable<Parking>> GetByLocationAsync(string location)
        {
            return await _collection.Find(p => p.Location == location).ToListAsync();
        }
    }
}

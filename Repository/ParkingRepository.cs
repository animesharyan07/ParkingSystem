using System.Collections.Generic;
using System.Threading.Tasks;
using DBSettings;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;

namespace Repository
{
    public class ParkingRepository(IOptions<ParkingDatabaseSetting> settings, IMongoClient mongoClient) : Repository<Parking>(settings, mongoClient), IParkingRepository
    {
        public async Task<IEnumerable<Parking>> GetByLocationAsync(string location)
        {
            return await _collection.Find(p => p.Location == location).ToListAsync();
        }
    }
}

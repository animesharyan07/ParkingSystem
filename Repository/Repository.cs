using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ParkingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingSystem.DBSettings;


namespace ParkingSystem.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public Repository(IOptions<ParkingDatabaseSetting> settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<T>(settings.Value.CollectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}

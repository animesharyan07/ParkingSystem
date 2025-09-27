    using Microsoft.Extensions.Options;
    using ParkingSystem.Models;
    using MongoDB.Driver;
    using ParkingSystem.DBSettings; // your DB settings namespace

    namespace ParkingSystem.Repository
    {
        public class UserRepository : IUserRepository
        {
            private readonly IMongoCollection<UserSignup> _users;

            public UserRepository(IOptions<ParkingDatabaseSetting> settings, IMongoClient mongoClient)
            {
                var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
                _users = database.GetCollection<UserSignup>("User"); // MongoDB collection name
            }

            // Get user by email
            public async Task<UserSignup?> GetByEmailAsync(string email)
            {
                return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            }

            // Create new user
            public async Task<UserSignup> CreateAsync(UserSignup user)
            {
                await _users.InsertOneAsync(user);
                return user;
            }
        }
    }

using System.Threading.Tasks;

using ParkingSystem.Models;

namespace ParkingSystem.Repository
{
    public interface IUserRepository
    {
        Task<UserSignup?> GetByEmailAsync(string email);
        Task<UserSignup> CreateAsync(UserSignup user);
    }
}

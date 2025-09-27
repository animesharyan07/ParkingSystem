using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.Models;
using ParkingSystem.Repository;

namespace ParkingSystem.Services
{
    public interface IUserService
    {
        Task<UserSignup> Signup(UserSignup user,string role);
        //Task<UserLogin> Login(UserLogin user, string password);
        Task<UserSignup> Login(string email, string password);
    }
}

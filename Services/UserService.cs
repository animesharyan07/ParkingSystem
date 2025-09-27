using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using ParkingSystem.Models;
using ParkingSystem.Services;
using ParkingSystem.Repository;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserSignup> Signup(UserSignup user, string role)
        {
           
            user.Role = role;

          
            var createdUser = await _userRepository.CreateAsync(user);
            return createdUser;
        }

       
        public async Task<UserSignup> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            if (user.Password != password)
                throw new Exception("Invalid password");

            return user;
            //return new UserLogin
            //{
            //    Email = user.Email,
            //    Password = user.Password,
              
            //};
        }
    }
}

using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models.DTOs;
using PizzaApplicationAPI.Models;
using System.Security.Cryptography;
using System.Text;
using PizzaApplicationAPI.Exceptions;
using System.Collections;

namespace PizzaApplicationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepo;

        public UserService(IRepository<int, User> userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<UserLoginDTO> Login(UserLoginDTO loginDTO)
        {
            var userDB = await _userRepo.Get(loginDTO.Id);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }

            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.PasswordHash);
            if (isPasswordSame)
            {
                loginDTO.Password = Encoding.UTF8.GetString(userDB.PasswordHash);
                return loginDTO;
            }
            throw new UnauthorizedUserException("Invalid username or password");
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<UserRegisterDTO> Register(UserRegisterDTO regiserDTO)
        {
            User user = null;
            try
            {
                user = MapRegiserDTOToUser(regiserDTO);
                user = await _userRepo.Add(user);
                if(user == null)
                {
                    throw new UnableToRegisterException("Not able to register at this moment");
                }
                regiserDTO.Password = Encoding.UTF8.GetString(user.PasswordHash);
                return regiserDTO;
            }
            catch (Exception ex) {
                throw new UnableToRegisterException($"Not able to register at this moment : {ex.Message}");
            }
            
        }


        private User MapRegiserDTOToUser(UserRegisterDTO regiserDTO)
        {
            User user = new User();
            user.Username = regiserDTO.Username;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.PasswordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(regiserDTO.Password));
            return user;
        }
    }
}

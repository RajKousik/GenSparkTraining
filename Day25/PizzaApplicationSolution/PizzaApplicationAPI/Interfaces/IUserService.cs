using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Interfaces
{
    public interface IUserService
    {
        public Task<UserLoginDTO> Login(UserLoginDTO loginDTO);
        public Task<UserRegisterDTO> Register(UserRegisterDTO registerDTO);

    }
}

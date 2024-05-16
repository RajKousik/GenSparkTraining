using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}

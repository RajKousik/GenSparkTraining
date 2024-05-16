using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

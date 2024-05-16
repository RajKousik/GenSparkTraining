using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models.DTOs
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

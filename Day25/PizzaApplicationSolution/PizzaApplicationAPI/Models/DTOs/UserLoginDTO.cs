using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}

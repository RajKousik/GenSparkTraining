using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApplicationAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordHashKey { get; set; }
    }

}

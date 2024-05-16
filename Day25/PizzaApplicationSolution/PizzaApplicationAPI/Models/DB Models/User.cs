using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaApplicationAPI.Models
{
    [Index(nameof(Username), IsUnique=true)]
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

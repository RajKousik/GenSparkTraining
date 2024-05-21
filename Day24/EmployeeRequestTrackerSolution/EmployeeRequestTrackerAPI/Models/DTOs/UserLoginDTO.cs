using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTrackerAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "User Id cannot be empty")]
        [Range(100, 999, ErrorMessage = "Invalid entry for User Id")]
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        [MinLength(1, ErrorMessage ="Too short"), MaxLength(15, ErrorMessage = "Too Long")]
        public string Password { get; set; } = string.Empty;
    }
}

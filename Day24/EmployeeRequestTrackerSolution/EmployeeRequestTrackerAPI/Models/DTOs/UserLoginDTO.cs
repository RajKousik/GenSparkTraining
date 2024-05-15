namespace EmployeeRequestTrackerAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

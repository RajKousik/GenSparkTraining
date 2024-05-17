namespace EmployeeRequestTrackerAPI.Models.DTOs
{
    public class EmployeeUserDTO 
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }
    }
}

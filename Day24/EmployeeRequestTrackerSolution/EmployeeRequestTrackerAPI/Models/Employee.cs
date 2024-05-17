namespace EmployeeRequestTrackerAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string? Role { get; set; }

        public ICollection<Request> RequestsRaised { get; set; }//No effect on the table
        public ICollection<Request> RequestsClosed { get; set; }//No effect on the table
    }
}

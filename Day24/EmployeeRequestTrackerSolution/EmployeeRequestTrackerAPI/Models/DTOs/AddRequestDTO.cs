using System.ComponentModel;

namespace EmployeeRequestTrackerAPI.Models.DTOs
{
    public class AddRequestDTO
    {
        public string RequestDescription { get; set; }

        public int RequestRaisedBy { get; set; }
    }
}

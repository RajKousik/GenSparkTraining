using System.ComponentModel.DataAnnotations;

namespace EmployeeRequestTrackerAPI.Models
{
    public class Request
    {

        public int Id{ get; set; }
        [Required]
        public string RequestDescription{ get; set; }
        [Required]
        public int RequestRaisedBy { get; set; }
        public int? RequestClosedBy{ get; set; }
        public DateTime RequestRaisedOn { get; set; } = DateTime.Now;
        public DateTime? RequestClosedOn { get; set; }
        public Employee RequestRaisedByEmployee { get; set; }
        public Employee? RequestClosedByEmployee { get; set; }
        public bool isRequestSolved { get; set; } = false;

    }
}

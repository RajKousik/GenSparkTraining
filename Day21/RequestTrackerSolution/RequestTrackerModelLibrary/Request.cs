using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class Request
    {
        [Key]
        public int RequestNumber { get; set; }
        public string RequestMessage { get; set; }
        public DateTime RequestDate { get; set; } = System.DateTime.Now;
        public DateTime? ClosedDate { get; set; } = null;
        public string RequestStatus { get; set; }


        public int RequestRaisedBy { get; set; }

        public Employee RaisedByEmployee { get; set; }

        public int? RequestClosedBy { get; set; } = null;


        public Employee RequestClosedByEmployee { get; set; }

        public ICollection<RequestSolution> RequestSolutions { get; set; }

        public override string ToString()
        {
            string result = "";
            result += $"Request Number: {RequestNumber}\n";
            result += $"Request Message: {RequestMessage}\n";
            result += $"Request Date: {RequestDate}\n";

            if (ClosedDate != null)
                result += $"Closed Date: {ClosedDate}\n";

            result += $"Request Status: {RequestStatus}\n";
            result += $"Raised By Employee : {RequestRaisedBy}";

            if(RaisedByEmployee != null)
                result += $" - {RaisedByEmployee.Name}\n";

            if (RequestClosedBy != null)
                result += $"Closed By Employee Id: {RequestClosedBy}\n";

            return result;
        }
    }
}

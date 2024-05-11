using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class SolutionFeedback
    {
        public int FeedbackId { get; set; }
        public float Rating { get; set; }
        public string? Remarks { get; set; }
        public int SolutionId { get; set; }
        public RequestSolution Solution { get; set; }
        public int FeedbackBy { get; set; }
        public Employee FeedbackByEmployee { get; set; }
        public DateTime FeedbackDate { get; set; }

        public override string ToString()
        {
            string result = $"Feedback ID: {FeedbackId}\n";
            result += $"Rating: {Rating}\n";
            result += $"Remarks: {Remarks ?? "N/A"}\n";
            result += $"Solution ID: {SolutionId}\n";
            result += $"Feedback By: {FeedbackBy}";

            if (FeedbackByEmployee != null)
            {
                result += $" - {FeedbackByEmployee.Name}\n";
            }

            result += $"Feedback Date: {FeedbackDate}\n";

            return result;
        }
    }
}

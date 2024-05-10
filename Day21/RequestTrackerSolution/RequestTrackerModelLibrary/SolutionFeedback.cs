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
    }
}

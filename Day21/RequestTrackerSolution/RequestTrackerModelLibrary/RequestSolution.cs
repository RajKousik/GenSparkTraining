using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class RequestSolution
    {
        [Key]
        public int SolutionId { get; set; }

        public int RequestId { get; set; }

        public Request RequestRaised { get; set; }

        public string SolutionDescription { get; set; }

        public int SolvedBy { get; set; }

        public Employee SolvedByEmployee { get; set; }

        public DateTime SolvedDate { get; set; }
        public bool IsSolved { get; set; } = false;
        public string? RequestRaiserComment { get; set; }

        public ICollection<SolutionFeedback> Feedbacks { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Solution ID: {SolutionId}");
            sb.AppendLine($"Request ID: {RequestId}");
            sb.AppendLine($"Solution Description: {SolutionDescription}");
            sb.AppendLine($"Solved By Employee ID: {SolvedBy}");
            sb.AppendLine($"Solved Date: {SolvedDate}");
            sb.AppendLine($"Is Solved: {IsSolved}");
            sb.AppendLine($"Request Raiser Comment: {RequestRaiserComment ?? "N/A"}");

            // Add logic to handle Feedbacks collection if needed

            return sb.ToString();
        }
    }
}

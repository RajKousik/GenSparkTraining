using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface ISolutionFeedbackBL
    {
        Task<SolutionFeedback> AddFeedback(SolutionFeedback feedback);

        Task<IList<SolutionFeedback>> GetAllAdminFeedbacks(int solutionId);

        Task<SolutionFeedback> GetFeedbackByFeedbackId(int userId);

        public Task<IList<SolutionFeedback>> GetFeedbackBySolutionId(int solutionId);


    }
}

using RequestTrackerBLLibrary.Interfaces;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary.BL
{
    public class SolutionFeedbackBL : ISolutionFeedbackBL
    {
        private readonly IRepository<int, SolutionFeedback> _solutionFeedbackRepository;

        public SolutionFeedbackBL()
        {
            IRepository<int, SolutionFeedback> repo = new SolutionFeedbackRepository(new RequestTrackerContext());
            _solutionFeedbackRepository = repo;
        }
        public async Task<SolutionFeedback> AddFeedback(SolutionFeedback feedback)
        {
            var result = await _solutionFeedbackRepository.Add(feedback);
            return result;
        }

        public async Task<IList<SolutionFeedback>> GetAllAdminFeedbacks(int solutionId)
        {
            var allFeedbacks = await _solutionFeedbackRepository.GetAll();

            var feedbacks = allFeedbacks
                                    .Where(f => f.SolutionId == solutionId)
                                    .ToList();

            return feedbacks;
        }

        public async Task<IList<SolutionFeedback>> GetFeedbackBySolutionId(int solutionId)
        {
            var allFeedbacks = await _solutionFeedbackRepository.GetAll();

            var feedbacks = allFeedbacks
                                    .Where(f => f.SolutionId == solutionId)
                                    .ToList();

            return feedbacks;
        }

        public async Task<SolutionFeedback> GetFeedbackByFeedbackId(int feedbackId)
        {
            var feedback = await _solutionFeedbackRepository.Get(feedbackId);
            return feedback;

        }
    }
}

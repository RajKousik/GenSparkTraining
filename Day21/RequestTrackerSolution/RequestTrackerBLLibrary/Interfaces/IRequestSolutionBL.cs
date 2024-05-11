using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary.Interfaces
{
    public interface IRequestSolutionBL
    {
        public Task<int> AddRequestSolution(RequestSolution requestSolution);
        public Task<IList<RequestSolution>> GetAllAdminRequestSolutions();
        public Task<IList<RequestSolution>> GetAllUserRequestSolutions(int employeeId, int requestNumber);
        public Task<RequestSolution> GetRequestSolution(int id);

        //public Task<IList<RequestSolution>> GetAllRequestSolutionByRequestId(int requestId);

        //public Task<RequestSolution> GetUserRequestSolution();
        public Task<RequestSolution> RespondToSolution(int requestSolutionId, string comment);

        public Task<RequestSolution> UpdateSolutionAsSolved(int solutionId);

        public Task<IList<RequestSolution>> GetSolutionsSolvedByEmployeeId(int employeeId);


    }
}

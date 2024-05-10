using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface IRequestSolutionBL
    {
        public Task<int> AddRequestSolution(RequestSolution requestSolution);
        public Task<IList<RequestSolution>> GetAllAdminRequestSolutions();
        public Task<IList<RequestSolution>> GetAllUserRequestSolutions(int employeeId, int requestNumber);
        public Task<RequestSolution> GetRequestSolution(int id);
        //public Task<RequestSolution> GetUserRequestSolution();
        public Task<RequestSolution> RespondToSolution(int requestSolutionId, string comment);




    }
}

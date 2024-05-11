using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface IRequestBL
    {
        public Task<int> OpenRequest(Request request);
        public Task<bool> CloseRequest(int RequestId, Employee employee);

        public Task<bool> UpdateRequest(int RequestId, string status);

        Task<Request> GetRequestById(int RequestId);

        Task<IList<Request>> GetAllRequests();

        Task<IList<Request>> GetAllRequestsById(int requestRaisedBy);

        public Task<IList<Request>> GetAllOpenRequests();

        public Task<IList<Request>> GetAllRequestsExcludeCurrentUser(int employeeId);

    }
}

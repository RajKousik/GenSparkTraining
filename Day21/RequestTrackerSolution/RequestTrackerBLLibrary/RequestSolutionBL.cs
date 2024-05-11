using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class RequestSolutionBL : IRequestSolutionBL
    {
        private readonly IRepository<int, RequestSolution> _requestSolutionRepository;
        private readonly IRequestBL _requestRepository;


        public RequestSolutionBL()
        {
            IRepository<int, RequestSolution> repo = new RequestSolutionRepository(new RequestTrackerContext());
            _requestSolutionRepository = repo;
        }

        public RequestSolutionBL(IRequestBL _requestBL)
        {
            IRepository<int, RequestSolution> repo = new RequestSolutionRepository(new RequestTrackerContext());
            _requestSolutionRepository = repo;
            _requestRepository = _requestBL;

        }

        public async Task<int> AddRequestSolution(RequestSolution requestSolution)
        {
            var result = await _requestSolutionRepository.Add(requestSolution);
            return result.SolutionId;
        }

        public async Task<RequestSolution> UpdateSolutionAsSolved(int solutionId)
        {
            var reqSolution = await _requestSolutionRepository.Get(solutionId);

            if (reqSolution != null)
            {
                reqSolution.IsSolved = true;
                await _requestSolutionRepository.Update(reqSolution);
            }
            return reqSolution;
        }

        public Task<RequestSolution> GetRequestSolution(int id)
        {
            var requestSolution = _requestSolutionRepository.Get(id);

            return requestSolution;
        }

        public async Task<IList<RequestSolution>> GetAllRequestSolutionByRequestId(int requestId)
        {
            var requestSolution = (await _requestSolutionRepository.GetAll()).ToList().FindAll(rs=>rs.RequestId == requestId);

            return requestSolution;
        }


        public async Task<IList<RequestSolution>> GetSolutionsSolvedByEmployeeId(int employeeId)
        {
            var requestSolution = (await _requestSolutionRepository.GetAll()).ToList().FindAll(rs => rs.SolvedBy == employeeId);
            return requestSolution;
        }


        public async Task<IList<RequestSolution>> GetAllAdminRequestSolutions()
        {
            IList<RequestSolution> requests = await _requestSolutionRepository.GetAll();
            if (requests.Count == 0)
            {
                return null;
            }
            return requests;
        }



        public async Task<IList<RequestSolution>> GetAllUserRequestSolutions(int userEmployee, int requestId)
        {
            var allUserRequests = await _requestRepository.GetAllRequests();

            var currentUserRequest = allUserRequests
                                    .Where(r => r.RequestRaisedBy == userEmployee && r.RequestNumber == requestId)
                                    .ToList();



            var solutions = new List<RequestSolution>();

            foreach (var request in currentUserRequest)
            {
                var userSolutions = request.RequestSolutions;
                solutions.AddRange(userSolutions);
            }

            return solutions;
        }


        

        public async Task<RequestSolution> RespondToSolution(int requestSolutionId, string comment)
        {
            var requestSolution = await _requestSolutionRepository.Get(requestSolutionId);

            if(requestSolution == null)
            {
                Console.Out.WriteLine("Error Happened...Try again");
                return null;
            }

            requestSolution.RequestRaiserComment = comment;

            var result = await _requestSolutionRepository.Update(requestSolution);

            return result;

        }
    }
}

using EmployeeRequestTrackerAPI.Exceptions;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Models.DTOs;

namespace EmployeeRequestTrackerAPI.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<int, Request> _repository;

        public RequestService(IRepository<int, Request> reposiroty)
        {
            _repository = reposiroty;
        }
        public async Task<AddRequestDTO> AddRequest(AddRequestDTO requestDto)
        {
            if(requestDto == null)
            {
                throw new UnableToAddRequestException();
            }
            try
            {
                Request request = MapRequestDtoToRequest(requestDto);
                var result = await _repository.Add(request);
                if(result != null)
                    return requestDto;
            }
            catch(Exception ex)
            {
                throw new UnableToAddRequestException();
            }
            return requestDto;
        }

        private Request MapRequestDtoToRequest(AddRequestDTO requestDto)
        {
            Request request = new Request();
            request.RequestDescription = requestDto.RequestDescription;
            request.RequestRaisedBy = requestDto.RequestRaisedBy;
            request.RequestRaisedOn = DateTime.Now;
            request.isRequestSolved = false;
            return request;

        }

        public async Task<IEnumerable<Request>> GetAdminRequests()
        {
            var requests = (await _repository.Get()).Where(r => r.isRequestSolved == false)
                                                    .OrderBy(r => r.RequestRaisedOn)
                                                    .ToList();
            if(requests.Count == 0)
            {
                throw new NoRequestsFoundException();
            }
            return requests;
        }

        public async Task<IEnumerable<Request>> GetUserRequests(int employeeId)
        {
            var requests = (await _repository.Get()).Where(r => r.RequestRaisedBy == employeeId)
                                                    .OrderBy(r => r.RequestRaisedOn)
                                                    .ToList();
            if (requests.Count == 0)
            {
                throw new NoRequestsFoundException();
            }
            return requests;
        }
    }
}

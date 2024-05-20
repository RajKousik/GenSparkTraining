using EmployeeRequestTrackerAPI.Exceptions;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Models.DTOs;
using System.Security.Claims;

namespace EmployeeRequestTrackerAPI.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<int, Request> _repository;

        public RequestService(IRepository<int, Request> reposiroty)
        {
            _repository = reposiroty;
        }
        public async Task<AddReturnRequestDTO> AddRequest(AddRequestDTO requestDto, int employeeId)
        {
            if(requestDto == null)
            {
                throw new UnableToAddRequestException();
            }
            try
            {
                Request request = MapRequestDtoToRequest(requestDto);
                request.RequestRaisedBy = employeeId;
                var result = await _repository.Add(request);
                AddReturnRequestDTO returnRequestDTO;
                if (result != null)
                {
                    returnRequestDTO = new AddReturnRequestDTO() { 
                                                RequestDescription = requestDto.RequestDescription,
                                                RequestRaisedBy = employeeId
                                                                };
                    return returnRequestDTO;
                }
            }
            catch(Exception ex)
            {
                throw new UnableToAddRequestException();
            }
            throw new UnableToAddRequestException();
        }

        private Request MapRequestDtoToRequest(AddRequestDTO requestDto)
        {
            Request request = new Request();
            request.RequestDescription = requestDto.RequestDescription;
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

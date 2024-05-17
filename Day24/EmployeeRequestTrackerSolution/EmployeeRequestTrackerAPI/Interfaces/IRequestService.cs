using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Models.DTOs;

namespace EmployeeRequestTrackerAPI.Interfaces
{
    public interface IRequestService
    {
        public Task<AddRequestDTO> AddRequest(AddRequestDTO requestDto);
        public Task<IEnumerable<Request>> GetUserRequests(int employeeId);
        public Task<IEnumerable<Request>> GetAdminRequests();
    }
}

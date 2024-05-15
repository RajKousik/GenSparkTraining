using EmployeeRequestTrackerAPI.Models.DTOs;
using EmployeeRequestTrackerAPI.Models;

namespace EmployeeRequestTrackerAPI.Interfaces
{
    public interface IUserService
    {
        public Task<Employee> Login(UserLoginDTO loginDTO);
        public Task<Employee> Register(EmployeeUserDTO employeeDTO);
    }
}

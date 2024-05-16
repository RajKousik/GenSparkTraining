using EmployeeRequestTrackerAPI.Models;

namespace EmployeeRequestTrackerAPI.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(Employee employee);
    }
}

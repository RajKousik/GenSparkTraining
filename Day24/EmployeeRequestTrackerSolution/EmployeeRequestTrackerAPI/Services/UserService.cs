using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models.DTOs;
using EmployeeRequestTrackerAPI.Models;
using System.Security.Cryptography;
using System.Text;
using EmployeeRequestTrackerAPI.Exceptions;

namespace EmployeeRequestTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepo;
        private readonly IRepository<int, Employee> _employeeRepo;

        public UserService(IRepository<int, User> userRepo, IRepository<int, Employee> employeeRepo)
        {
            _userRepo = userRepo;
            _employeeRepo = employeeRepo;
        }
        public async Task<Employee> Login(UserLoginDTO loginDTO)
        {
            var userDB = await _userRepo.Get(loginDTO.UserId);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                var employee = await _employeeRepo.Get(loginDTO.UserId);
                if (userDB.Status == "Active")
                    return employee;
                throw new UserNotActiveException("Your account is not activated");
            }
            throw new UnauthorizedUserException("Invalid username or password");
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<Employee> Register(EmployeeUserDTO employeeDTO)
        {
            Employee employee = null;
            User user = null;
            try
            {
                employee = employeeDTO as Employee;
                user = MapEmployeeUserDTOToUser(employeeDTO);
                employee = await _employeeRepo.Add(employee);
                //user.EmployeeId = employee.Id;
                user = await _userRepo.Add(user);
                ((EmployeeUserDTO)employee).Password = string.Empty;
                return employee;
            }
            catch (Exception) { }
            if (employee != null)
                await RevertEmployeeInsert(employee);
            if (user != null && employee == null)
                await RevertUserInsert(user);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        private async Task RevertUserInsert(User user)
        {
            await _userRepo.Delete(user.EmployeeId);
        }

        private async Task RevertEmployeeInsert(Employee employee)
        {
            await _employeeRepo.Delete(employee.Id);
        }

        private User MapEmployeeUserDTOToUser(EmployeeUserDTO employeeDTO)
        {
            User user = new User();
            user.EmployeeId = employeeDTO.Id;
            user.Status = "Disabled";
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(employeeDTO.Password));
            return user;
        }
    }
}

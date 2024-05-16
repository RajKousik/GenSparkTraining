using RequestTrackerDALLibrary;
//using RequestTrackerModelLibrary;
using RequestTrackerDALLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class EmployeeBL : IEmployeeService
    {
        readonly IRepository<int, Employee> _employeeRepository;

        public EmployeeBL()
        {
            _employeeRepository = new EmployeeRepository();
        }
        public int? AddEmployee(Employee employee)
        {
            Employee? result = null;
            result = _employeeRepository.Add(employee);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateEmployeeException();
        }

        public bool DeleteEmployee(int id)
        {
            var deletedEmployee = _employeeRepository.Delete(id);
            if (deletedEmployee != null)
            {
                return true;
            }
            throw new EmployeeNotFoundException();
        }

        public Employee GetEmployeeById(int id)
        {
            var result = _employeeRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new EmployeeNotFoundException();
        }

        public Employee GetEmployeeByName(string name)
        {
            var employee = _employeeRepository.GetAll().Find(e => e.Name == name);
            if (employee == null)
            {
                throw new EmployeeNotFoundException();
            }
            return employee;
        }

        public List<Employee> GetEmployeeList()
        {
            var employees = _employeeRepository.GetAll();
            if (employees == null)
            {
                throw new EmployeeNotFoundException();
            }
            return employees;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var result = _employeeRepository.Update(employee);
            if (result != null)
            {
                return result;
            }
            throw new EmployeeNotFoundException();
        }
    }
}

//using RequestTrackerModelLibrary;
using RequestTrackerDALLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public interface IEmployeeService
    {
        int? AddEmployee(Employee employee);
        Employee GetEmployeeById(int id);

        Employee GetEmployeeByName(string name);

        Employee UpdateEmployee(Employee employee);

        List<Employee> GetEmployeeList();

        bool DeleteEmployee(int id);
    }
}

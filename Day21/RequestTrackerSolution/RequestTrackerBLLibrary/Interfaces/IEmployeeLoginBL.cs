using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary.Interfaces
{
    public interface IEmployeeLoginBL
    {
        public Task<Employee> Login(Employee employee);
        public Task<Employee> Register(Employee employee);
    }
}

using Microsoft.EntityFrameworkCore;
using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class EmployeeRequestRepository : EmployeeRepository
    {
        public EmployeeRequestRepository(RequestTrackerContext context) : base(context)
        {

        }

        public async override Task<IList<Employee>> GetAll()
        {
            return await _context.Employees.Include(e=>e.RequestsRaised).ToListAsync();
        }

        public async override Task<Employee> Get(int key)
        {
            var emp =  _context.Employees.Include(e => e.RequestsRaised).SingleOrDefault(e => e.Id == key);
            return emp;
        }
    }
}

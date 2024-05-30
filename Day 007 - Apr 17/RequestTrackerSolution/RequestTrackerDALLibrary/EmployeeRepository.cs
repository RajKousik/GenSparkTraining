using RequestTrackerDALLibrary.Model;
//using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        RequestTrackerAppContext context = new RequestTrackerAppContext();
        private List<Employee> _employees;


        public EmployeeRepository()
        {
            _employees = context.Employees.ToList();

        }

        //int GenerateId()
        //{
        //    if (_employees.Count == 0)
        //    {
        //        return 1;
        //    }
        //    int id = _employees.Count;
        //    return ++id;
        //}

        public Employee Add(Employee item)
        {
            context.Employees.Add(item);
            context.SaveChanges();
            _employees = context.Employees.ToList();
            if (_employees.Contains(item)) return item;
            return null;
        }

        public Employee Delete(int key)
        {
            _employees = context.Employees.ToList();
            var employee = _employees.SingleOrDefault(d => d.Id == key);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
                _employees = context.Employees.ToList();
                return employee;
            }
            return null;
        }

        public List<Employee> GetAll()
        {
            if (_employees.Count == 0)
                return null;
            return _employees;
        }

        public Employee Get(int key)
        {
            var employee = _employees.SingleOrDefault(d => d.Id == key);
            if (employee != null)
            {
                return employee;
            }
            return null;
        }

        public Employee Update(Employee item)
        {
            _employees = context.Employees.ToList();
            var employee = _employees.SingleOrDefault(d => d.Id == item.Id);
            if (employee != null)
            {
                employee = item;
                context.Employees.Update(employee);
                context.SaveChanges();
                _employees = context.Employees.ToList();
                return employee;
            }
            return null;
        }

    }
}

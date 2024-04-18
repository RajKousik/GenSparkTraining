using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerDALLibrary
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        readonly Dictionary<int, Employee> _employees;

        public EmployeeRepository()
        {
            _employees = new Dictionary<int, Employee>();
        }

        int GenerateId()
        {
            if (_employees.Count == 0)
            {
                return 1;
            }
            int id = _employees.Keys.Max();
            return ++id;
        }

        public Employee Add(Employee item)
        {
            if (_employees.ContainsValue(item))
            {
                return null;
            }
            int id = GenerateId();
            item.Id = id;
            _employees.Add(id, item);
            return item;
            
        }

        public Employee Delete(int key)
        {
            if (_employees.ContainsKey(key))
            {
                var employee = _employees[key];
                _employees.Remove(key);
                return employee;
            }
            return null;
        }

        public Employee Get(int key)
        {
            return _employees.ContainsKey(key) ? _employees[key] : null;
        }

        public List<Employee> GetAll()
        {
            if (_employees.Count == 0)
            {
                return null;
            }
            return _employees.Values.ToList();
        }

        public Employee Update(Employee item)
        {
            if (_employees.ContainsKey(item.Id))
            {
                _employees[item.Id] = item;
                return item;
            }
            return null;
        }
    }
}

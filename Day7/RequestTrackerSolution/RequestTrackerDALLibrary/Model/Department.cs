using System;
using System.Collections.Generic;

namespace RequestTrackerDALLibrary.Model
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? DepartmentHead { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            return "\nDeaprtment Id : " + Id
                + "\nDepartment Name : " + Name
                + "\nDepartment Head Id: " + DepartmentHead + "\n";
        }
    }
}

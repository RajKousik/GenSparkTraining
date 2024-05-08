using System;
using System.Collections.Generic;

namespace RequestTrackerDALLibrary.Model
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }
        public double? Salary { get; set; }
        public string? Role { get; set; }
        public int? EmployeeDepartment { get; set; }

        public virtual Department? EmployeeDepartmentNavigation { get; set; }
    }
}

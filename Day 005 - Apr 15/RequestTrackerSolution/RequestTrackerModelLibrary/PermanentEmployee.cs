using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class PermanentEmployee:Employee
    {
        /// <summary>
        /// Default Constrcutor for permanent Employee
        /// </summary>
        public PermanentEmployee()
        {
            Type = "PermanentEmployee";
        }


        /// <summary>
        /// Parameterized constructor for permanent Employee
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <param name="name">Name of the employee</param>
        /// <param name="dateOfBirth">DOB of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        /// <param name="wagesPerDay">WagesPerDay of the employee</param>
        public PermanentEmployee(int id, string name, DateTime dateOfBirth, double salary):base(id, name, dateOfBirth)
        {
            Type = "PermanentEmployee";
            Salary = salary;

        }

        /// <summary>
        /// Overrides the base class method and calculates the salary from WagesPerDay
        /// </summary>
        public override void BuildEmployeeFromConsole()
        {
            base.BuildEmployeeFromConsole();
            Console.WriteLine("Please enter the Basic Salary");
            Salary = Convert.ToDouble(Console.ReadLine());
        }
        public override void PrintEmployeeDetails()
        {
            base.PrintEmployeeDetails();
            Console.WriteLine("Employee Salary : Rs." + Salary);
        }

        public override string ToString()
        {
            return base.ToString() +
                "\nEmployee Salary: Rs." + Salary;
        }

    }
}

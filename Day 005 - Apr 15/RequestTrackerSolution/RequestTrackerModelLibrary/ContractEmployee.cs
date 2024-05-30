using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class ContractEmployee:Employee
    {
        public double WagesPerDay { get; set; }

        /// <summary>
        /// Default Constrcutor for contract Employee
        /// </summary>
        public ContractEmployee()
        {
            WagesPerDay = 0;
            Type = "ContractEmployee";
            Console.WriteLine("Contract employee constructor");
        }

        /// <summary>
        /// Parameterized constructor for Contract Employee
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <param name="name">Name of the employee</param>
        /// <param name="dateOfBirth">DOB of the employee</param>
        /// <param name="salary">Salary of the employee</param>
        /// <param name="wagesPerDay">WagesPerDay of the employee</param>
        public ContractEmployee(int id, string name, DateTime dateOfBirth, double salary, double wagesPerDay) : base(id, name, dateOfBirth)
        {
            Type = "ContractEmployee";
            Console.WriteLine("Contract Employee class prameterized constructor");
            WagesPerDay = wagesPerDay;
        }

        /// <summary>
        /// Overrides the base class method and calculates the salary from WagesPerDay
        /// </summary>
        public override void BuildEmployeeFromConsole()
        {
            base.BuildEmployeeFromConsole();
            Console.WriteLine("Please enter the Per Day Wage");
            WagesPerDay = Convert.ToDouble(Console.ReadLine());
            CalculateSalary();
        }

        /// <summary>
        /// Helper function to calculate the salary from wages
        /// </summary>
        private void CalculateSalary()
        {
            Salary = WagesPerDay * 30;
        }

        /// <summary>
        /// Overrides the base class PrintEmployeeDetails function 
        /// </summary>
        public override void PrintEmployeeDetails()
        {
            base.PrintEmployeeDetails();
            Console.WriteLine("Wages/Day : " + WagesPerDay);
            Console.WriteLine("Salary : " + Salary);
        }


        public override string ToString()
        {
            return base.ToString() +
                "\nWages / Day : " + WagesPerDay;
        }

    }
}

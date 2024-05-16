using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApplicationModelLibrary
{
    public class CtsEmployee : Employee
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CtsEmployee()
        {
            CompanyName = "CTS";
        }

        /// <summary>
        /// Parameterized Constrcutor
        /// </summary>
        /// <param name="empId">Employee Id</param>
        /// <param name="empName">Employee Name</param>
        /// <param name="department">Deaprtment</param>
        /// <param name="designation">Designation</param>
        /// <param name="basicSalary">Basic Salary</param>
        /// <param name="serviceCompleted">Service Completed</param>
        public CtsEmployee(int empId, string empName, string department, string designation, double basicSalary, float serviceCompleted) : base(empId, empName, department, designation, basicSalary, serviceCompleted)
        {
            CompanyName = "CTS";
        }

        /// <summary>
        /// Calculating Employee PF
        /// </summary>
        /// <returns>return calculating PF</returns>
        public override double EmployeePF()
        {
            double EmployeePF = BasicSalary * 0.12;
            double EmployerContribution = BasicSalary * 0.12;

            return EmployeePF + EmployerContribution;

        }

        /// <summary>
        /// calculating the gratuity amount
        /// </summary>
        /// <returns>returns the gratuity amount</returns>
        public override double GratuityAmount()
        {
            if (ServiceCompleted > 20)
                return 3 * BasicSalary;
            else if (ServiceCompleted > 10)
                return 2 * BasicSalary;
            else if (ServiceCompleted > 5)
                return BasicSalary;
            else 
                return 0.0;
        }

        /// <summary>
        /// Calculates the leave details
        /// </summary>
        /// <returns>Returns leave details</returns>
        public override string LeaveDetails()
        {
            return "1 day of Casual Leave per month\r\n" +
                   "12 days of Sick Leave per year\r\n" +
                   "10 days of Privilege Leave per year";
        }
    }
}

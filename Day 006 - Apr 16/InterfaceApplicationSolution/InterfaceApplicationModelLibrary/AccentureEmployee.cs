using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApplicationModelLibrary
{
    public class AccentureEmployee : Employee
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccentureEmployee() 
        {
            CompanyName = "Accenture";
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
        public AccentureEmployee(int empId, string empName, string department, string designation, double basicSalary, float serviceCompleted) : base(empId, empName, department, designation, basicSalary,
            serviceCompleted)
        {
            CompanyName = "Accenture";
        }

        /// <summary>
        /// Calculating Employee PF
        /// </summary>
        /// <returns>return calculating PF</returns>
        public override double EmployeePF()
        {
            double EmployeePF = BasicSalary * 0.12;
            double EmployerContribution = BasicSalary * 0.0833;
            double PensionFund = BasicSalary * 0.0367;
            
            return EmployeePF + EmployerContribution + PensionFund;

        }

        /// <summary>
        /// calculating the gratuity amount
        /// </summary>
        /// <returns>returns the gratuity amount</returns>
        public override double GratuityAmount()
        {
            return 0.0;
        }

        /// <summary>
        /// Calculates the leave details
        /// </summary>
        /// <returns>Returns leave details</returns>
        public override string LeaveDetails()
        {
            return "2 days of Casual Leave per month\r\n" +
                   "5 days of Sick Leave per year\r\n" +
                   "5 days of Privilege Leave per year";
        }
    }
}

using InterfaceApplicationModelLibrary;

namespace InterfaceApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Employee employee1 = new AccentureEmployee(101, "Raj", "Technical", "Engineer", 10000, 5);
            Employee employee2 = new CtsEmployee(102, "RAJ RK", "Technical", "Analyst", 30000, 15);
            //employee1.PrintEmployeeDetails();
            //employee2.PrintEmployeeDetails();
            employee2.PrintEmployeeDetails();
            Company company = new Company();
            company.CompanyGovtRules(employee2);



            //string LeaveDetails = employee2.LeaveDetails();
            //double GratuityAmount = employee2.GratuityAmount(employee2.ServiceCompleted, employee2.BasicSalary);
            //Console.WriteLine(LeaveDetails);
            //Console.WriteLine(GratuityAmount);

        }
    }
}

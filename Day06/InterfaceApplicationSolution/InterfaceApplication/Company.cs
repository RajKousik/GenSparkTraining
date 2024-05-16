using InterfaceApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApplication
{
    internal class Company
    {
        public void CompanyGovtRules(IGovtRules govtRules)
        {
            Console.WriteLine("Employee PF:");
            Console.WriteLine(govtRules.EmployeePF());
            Console.WriteLine("Gratuity:");
            Console.WriteLine(govtRules.GratuityAmount());
            Console.WriteLine("Leave Details:");
            Console.WriteLine(govtRules.LeaveDetails());

        }
    }
}

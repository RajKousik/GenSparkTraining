using InterfaceApplicationModelLibrary;

namespace InterfaceApplication
{

    class UnderstandingIndexers
    {
        public string[] vals = new string[3];

        public string this[int index, bool isTrue]
        {
            get { return vals[index]; }
            set {
                if(isTrue)
                    vals[index] = value + " is true"; 
                else
                    vals[index] = value + 2;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            UnderstandingIndexers indexers = new UnderstandingIndexers();
            //string[] newVals = { "Hi", "Hello", "hey" };

            //indexers.vals = newVals;

            indexers[0, false] = "Hi";
            indexers[1, true] = "hello";
            indexers[2, true] = "hey";

            foreach (string val in indexers.vals)
            {
                Console.WriteLine(val);
            }















            //Employee employee1 = new AccentureEmployee(101, "Raj", "Technical", "Engineer", 10000, 5);
            //Employee employee2 = new CtsEmployee(102, "RAJ RK", "Technical", "Analyst", 30000, 15);
            //employee1.PrintEmployeeDetails();
            //employee2.PrintEmployeeDetails();
            //employee2.PrintEmployeeDetails();
            //Company company = new Company();
            //company.CompanyGovtRules(employee2);



            //string LeaveDetails = employee2.LeaveDetails();
            //double GratuityAmount = employee2.GratuityAmount(employee2.ServiceCompleted, employee2.BasicSalary);
            //Console.WriteLine(LeaveDetails);
            //Console.WriteLine(GratuityAmount);

        }
    }
}

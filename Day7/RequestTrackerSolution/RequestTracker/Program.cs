using RequestTrackerModelLibrary;
using System.Collections;
using System.Security.Cryptography;

namespace RequestTracker
{
    internal class Program
    {
        void UnderstandingGenericList()
        {
            List<int> numbers = new List<int>();
            numbers.Add(100);
            numbers.Add(79);
            numbers.Add(55);
            numbers.Add(55);
            double total = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine(numbers[i]);
                total += numbers[i];
            }
            Console.WriteLine($"Total is {total}");
        }

        void UnderstaingList()
        {
            ArrayList list = new ArrayList();
            list.Add(100);
            list.Add("Hello");
            list.Add(23.4);
            list.Add(90.3f);
            double total = 0;
            list.Add(new Employee(101, "Ramu", new DateTime(), "Admin"));
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
                //total = Convert.ToDouble(list[i]);
            }
        }


        void UnderstandingSet()
        {
            HashSet<string> names = new HashSet<string>()
            {
                "Ramu","Bimu"
            };
            names.Add("Somu");
            names.Add("Komu");
            names.Add("Timu");
            names.Add("Ramu");
            Console.WriteLine(names.Count);
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }

        void UnderstandingDictionary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(101, "Raj");
            dict.Add(102, "JV");
            dict.Add(103, "Ash");
            dict.Add(104, "Raj");

            //Console.WriteLine(dict.Values);

            foreach (var item in dict.Values)
            {
                Console.WriteLine(item);
                
            }

            if(dict.ContainsKey(102))
                Console.WriteLine(dict[102]);
            if(dict.ContainsValue("DHoni"))
                Console.WriteLine("Yes you are Dhoni");
        }

        static void Main(string[] args)
        {
            //Employee employee1, employee2;

            //employee1 = new Employee(101, "Ramu", new DateTime(2000, 12, 2), "Admin");
            //employee2 = new Employee(101, "Ramu", new DateTime(2000, 12, 2), "Admin");
            //if (employee1 == employee2)
            //{
            //    Console.WriteLine("Both Same");
            //}
            //else
            //{
            //    Console.WriteLine($"{employee1} and {employee2} are Not same employee");
            //}
            
            Program program = new Program();
            //program.UnderstaingList();
            //program.UnderstandingGenericList();
            //program.UnderstandingSet();
            program.UnderstandingDictionary();
            

        }
    }
}

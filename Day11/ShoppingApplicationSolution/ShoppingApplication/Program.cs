using ShoppingApplicationModelLibrary;
using ShoppingDALLibrary;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingApplication
{
    internal class Program
    {
        public delegate T calcDel<T>(T n1, T n2);//creating a type that refferes to a method
        //void Calculate(calcDel<int> cal)
        //{
        //    int n1 = 20, n2 = 10;
        //    int result = cal(n1, n2);
        //    Console.WriteLine($"The result of {n1} and {n2} is {result}");
        //}
        
        void Calculate(Func<int, int, int> cal)
        {
            int n1 = 20, n2 = 10;
            int result = cal(n1, n2);
            Console.WriteLine($"The result of {n1} and {n2} is {result}");
        }
        public int Add(int num1, int num2)
        {
            return (num1 + num2);
        }

        public int Sub(int num1, int num2)
        { 
            return (num1 - num2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();

            //calcDel c1 = new calcDel(program.Add);
            //calcDel<int> c1 = new calcDel<int>(program.Add);
            //calcDel<int> c2 = delegate (int num1, int num2)
            //{
            //    return (num1 - num2);
            //};
            //calcDel<int> c3 = (int num1, int num2)=> (num1 * num2);
            //program.Calculate(c1);
            //program.Calculate(c2);
            //program.Calculate(c3);

            //Func<int, int, int> c1 = (num1, num2) => (num1 + num2);
            //program.Calculate(c1);


            //int[] numbers = { 40, 56, 23, 54, 12, 32 };

            //var anotherArray = from n in numbers where n > 30 select n;
            //var another = numbers.Where((n) => n > 30);
            //var another = numbers.OrderBy(n => n);

            //foreach (var item in another)
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in anotherArray)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("------");

            //IRepository<int, Customer> customerRepo = new CustomerRepository();
            //customerRepo.Add(new Customer { Id = 1, Name = "Ramu", Age = 23 });
            //customerRepo.Add(new Customer { Id = 2, Name = "Ramu", Age = 23 });
            //customerRepo.Add(new Customer { Id = 3, Name = "Komu", Age = 27 });
            //var customers = customerRepo.GetAll().ToList();
            ////customers.Sort(new SortByCustomerName());

            //customers = customers.OrderByDescending(cust => cust.Name).ThenByDescending(cust=>cust.Age).ThenByDescending(cust=>cust.Id).ToList();

            //foreach (var item in customers)
            //{
            //    Console.WriteLine(item);
            //}


            //int[] numbers = { 89, 78, 23, 546, 787, 98, 11, 3 };

            //int[] evenNumebrs = numbers.EvenCatch();
            //foreach (int n in evenNumebrs)
            //    Console.WriteLine(n);
            //string message = "Hello World";
            //message = message.Reverse();
            //Console.WriteLine(message);

            UnderstandingLINQ();

        }

        public static void UnderstandingLINQ()
        {
            IList<Customer> customerList = new List<Customer>() {
                new Customer() { Id = 1, Name = "RAJ", Age = 18} ,
                new Customer() { Id = 2, Name = "EMILIA",  Age =  19} ,
                new Customer() { Id = 3, Name = "MARTHA",  Age = 18 } ,
                new Customer() { Id = 4, Name = "ADAM" , Age = 25} ,
                new Customer() { Id = 5, Name = "EVE" , Age = 19 },
                new Customer() { Id = 6, Name = "KATHERINE" , Age = 19 }
            };

            //var filteredResult = from c in customerList
            //                     where c.Age >= 15 && c.Age < 20
            //                     select c.Name;


            //var filteredResult = customerList.Where((s, i) => {
            //    if (i % 2 == 0) // if it is even element
            //        return true;

            //    return false;
            //});
            //IList mixedList = new ArrayList();
            //mixedList.Add("RAM");
            //mixedList.Add(1);
            //mixedList.Add("JANU");
            //mixedList.Add(false);
            //mixedList.Add(new Customer() { Id = 1, Age = 13, Name = "RAJ", Phone = "8972910123" });
            //var filteredResult = from c in mixedList.OfType<Customer>() select c;


            //var filteredResult = customerList.OrderByDescending(c => c.Name);

            //var filteredResult = from c in customerList orderby c.Name descending select c;



            //var filteredResult = from c in customerList group c by c.Age;

            //foreach (var groupedObject in filteredResult)
            //{
            //    Console.WriteLine("Age of {0}:", groupedObject.Key);

            //    foreach(var c in groupedObject)
            //        Console.WriteLine(c);
            //    Console.WriteLine();
            //}

            //var filteredResult = from c in customerList select new {
            //    Name = "Mr/Mrs:" + c.Name,
            //    Id = "CN" + c.Id
            //};

            //Customer newCustomer = new Customer() { Id = 3, Name = "MARTHA" };
            //bool isContains = customerList.Contains(newCustomer, new customSeperator());

            //Console.WriteLine(isContains);

            //foreach(var customer in filteredResult)
            //{
            //    Console.WriteLine(customer.Name);
            //    Console.WriteLine(customer.Id);
            //}

            //AGGREGAE FUNCTIONS
            //string commaSeperatedNames = customerList.Aggregate<Customer, String>("Customer Names: ", (str, s)=>  str = str + s.Name + ", ");
            //Console.WriteLine(commaSeperatedNames);

            //int totalAgeOfCustomers = customerList.Aggregate<Customer, int>(0, (totalAge,s)=> totalAge += s.Age);
            //Console.WriteLine(totalAgeOfCustomers);

            //string commaSeperatedNames = customerList.Aggregate<Customer, string, string>(
            //                                                String.Empty, 
            //                                                (str, s) => str += s.Name + ",", 
            //                                                str => str.Substring(0, str.Length - 1)
            //                                                );
            //Console.WriteLine(commaSeperatedNames);


            var averageAge = customerList.Average(c => c.Age);
            Console.WriteLine(averageAge);

            
        }


    }

    public class customSeperator : IEqualityComparer<Customer>
    {

        public bool Equals(Customer? x, Customer? y)
        {
            return (x.Id == y.Id && x.Name == y.Name);
        }

        public int GetHashCode([DisallowNull] Customer obj)
        {
            return obj.GetHashCode();
        }
    }




    public static class StringMethods
    {
        public static string Reverse(this string msg)
        {
            char[] chars = msg.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

    }
    public static class NumberExtension
    {
        public static int[] EvenCatch(this int[] nums)
        {
            List<int> result = new List<int>();
            foreach (int num in nums)
                if (num % 2 == 0)
                    result.Add(num);
            return result.ToArray();
        }
    }
}

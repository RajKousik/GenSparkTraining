using UnderstandingBasicsApp.Models;

namespace UnderstandingBasicsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //Employee employee1 = new Employee
            //{
            //    Name = "Test",
            //    Email = "someone@gmail.com",
            //    DateOfBirth = new DateTime(2003, 03, 13), 
            //    Salary = 10000
            //};
            //string employee1Message = employee1.StartWork(8);
            //Console.WriteLine(employee1Message); 
            //employee1.PrintEmployeeDetails();

            //Console.WriteLine("\n");

            ////  Employee2
            //Employee employee2 = new Employee();
            //string employee2Message = employee2.StartWork(9);
            //Console.WriteLine(employee2Message);
            //employee2.PrintEmployeeDetails();

            //Console.WriteLine("\n");

            //// Employee3
            //Employee employee3 = new Employee(20, "Raj", new DateTime(2000, 1, 1), "raj@gmail.com", 20000.00);
            //string employee3Message = employee3.StartWork(7);
            //Console.WriteLine(employee3Message);
            //employee3.PrintEmployeeDetails();

            //Console.WriteLine("\n");

            //int[] scores = { 1, 2, 3, 4, 5};

            //int i = 0;
            //// For Loop
            //for (i = 0; i < scores.Length; i++)
            //{
            //    Console.WriteLine(scores[i]);
            //}


            ////While Loop
            //i = 0;
            //while(i < scores.Length)
            //{
            //    Console.WriteLine(scores[i]);
            //    i++;
            //}


            //// Do While Loop
            //i = 0;
            //do
            //{
            //    Console.WriteLine(scores[i]);
            //}while(i++ < scores.Length);







            const int EmployeeCount = 3;
            Program program = new Program();
            Employee[] employees = new Employee[EmployeeCount];
            for (int i = 0; i < employees.Length; i++)
            {
                Random rnd = new Random();
                employees[i] = program.CreateEmployee(rnd.Next(1, 1000));
            }

            foreach (Employee employee in employees)
            {
                employee.PrintEmployeeDetails();
            }

        }

        Employee CreateEmployee(int id)
        {
            
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("DOB: ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Salary: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            Employee newEmployee = new Employee(id, name, dob, email, salary);
            return newEmployee;

        }
    }
}

using System;

namespace RequestTrackerModelLibrary
{
    public class Employee : IClientInteraction, IInternalCompanyWorking
    {
        public Department EmployeeDepartment { get; set; }
        int age;
        DateTime dob;
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age
        {
            get
            {
                return age;
            }
        }
        public DateTime DateOfBirth
        {
            get => dob;
            set
            {
                dob = value;
                age = ((DateTime.Today - dob).Days) / 365;
            }
        }
        public double Salary { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>

        /// <summary>
        /// Parameterised constructor
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name as string</param>
        /// <param name="dateOfBirth">Employee DOB as DateTime</param>
        /// <param name="salary">Employee Salary</param>
        public Employee()
        {
            //Console.WriteLine("Employee class default constructor");
            Id = 0;
            Name = string.Empty;
            Salary = 0.0;
            DateOfBirth = new DateTime();
            Type = string.Empty;
        }

        public Employee(int id, string name, DateTime dateOfBirth)
        {
            Console.WriteLine("Employee class prameterized constructor");
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Creates new employee object from console
        /// </summary>
        public virtual void BuildEmployeeFromConsole()
        {
            Console.WriteLine("Please enter the Name");
            Name = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Please enter the Date of birth");
            DateOfBirth = Convert.ToDateTime(Console.ReadLine());
        }

        /// <summary>
        /// Prints details of an employee
        /// </summary>
        public virtual void PrintEmployeeDetails()
        {
            Console.WriteLine("Employee Id : " + Id);
            Console.WriteLine("Employee Type : " + Type);
            Console.WriteLine("Employee Name : " + Name);
            Console.WriteLine("Employee Age : " + Age);
            Console.WriteLine("Date of birth : " + DateOfBirth);
        }


        public override string ToString()
        {
            return "Employee Id : " + Id +
                "\nEmployee Type : " + Type +
                "\nEmployee Name : " + Name +
                "\nEmployee Age : " + Age +
                "\nDate of birth : " + DateOfBirth;
        }

        public void GetOrder()
        {
            Console.WriteLine("Order Fetched by " + Name);
        }

        public void GetPayment()
        {
            Console.WriteLine("Get the payment as per terms");
        }

        public void RaiseRequest()
        {
            throw new NotImplementedException();
        }

        public void CloseRequest()
        {
            throw new NotImplementedException();
        }
    }
}

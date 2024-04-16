namespace RequestTrackerModelLibrary
{
    public class Employee
    {
        int age;
        DateTime dob;
        public int Id { get; set; }
        public string Name { get; set; }
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

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Employee()
        {
            Id = 0;
            Name = string.Empty;
            Salary = 0.0;
            DateOfBirth = new DateTime();
        }

        /// <summary>
        /// Parameterised constructor
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="name">Employee name as string</param>
        /// <param name="dateOfBirth">Employee DOB as DateTime</param>
        /// <param name="salary">Employee Salary</param>
        public Employee(int id, string name, DateTime dateOfBirth, double salary)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Salary = salary;
        }

        /// <summary>
        /// Creates new employee object from console
        /// </summary>
        public void BuildEmployeeFromConsole()
        {
            Console.WriteLine("Please enter the Name");
            Name = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Please enter the Date of birth");
            DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Please enter the Basic Salary");
            Salary = Convert.ToDouble(Console.ReadLine());
        }

        /// <summary>
        /// Prints details of an employee
        /// </summary>
        public void PrintEmployeeDetails()
        {
            Console.WriteLine("Employee Id : " + Id);
            Console.WriteLine("Employee Name " + Name);
            Console.WriteLine("Date of birth : " + DateOfBirth);
            Console.WriteLine("Employee Age : " + Age);
            Console.WriteLine("Employee Salary : Rs." + Salary);
        }

    }
}

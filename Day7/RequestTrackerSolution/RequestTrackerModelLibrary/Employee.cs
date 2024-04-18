namespace RequestTrackerModelLibrary
{
    public class Employee
    {
        public Department EmployeeDepartment { get; set; }
        public int age;
        public DateTime dob;
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
        public string Role { get; set; }

        public Employee()
        {
            Id = 0;
            Name = string.Empty;
            Salary = 0.0;
            DateOfBirth = new DateTime();
            Role = "Employee";
            EmployeeDepartment = new Department();
        }
        public Employee(string name, DateTime dateOfBirth, double salary, string role)
        {
            //Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Role = role;
            Salary = salary;
            //EmployeeDepartment = employeeDepartment;
        }

        public virtual void BuildEmployeeFromConsole()
        {
            Console.WriteLine("Please enter the Name");
            Name = Console.ReadLine() ?? String.Empty;
            Console.WriteLine("Please enter the Date of birth");
            DateOfBirth = Convert.ToDateTime(Console.ReadLine());
            Role = "Employee";
        }


        public override string ToString()
        {
            return "\nEmployee Id : " + Id
                + "\nEmployee Name " + Name
                + "\nDate of birth : " + DateOfBirth
                + "\nAge : " + Age
                + "\nEmployee Role " + Role + "\n";
        }
        public override bool Equals(object? obj)
        {
            Employee e1, e2;
            e1 = this;
            //e2 = (Employee)obj;//Casting
            e2 = obj as Employee;//Casting in a more symanctic way
            return e1.Id.Equals(e2.Id);
        }

    }
}

namespace InterfaceApplicationModelLibrary
{
    abstract public class Employee : IGovtRules
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty ;
        public string Designation { get; set; } = string.Empty ;
        public double BasicSalary { get; set; }

        public float ServiceCompleted { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="empId">Employee Id</param>
        /// <param name="empName">Employee Name</param>
        /// <param name="department">Deaprtment</param>
        /// <param name="designation">Designation</param>
        /// <param name="basicSalary">Basic Salary</param>
        /// <param name="serviceCompleted">Service Completed</param>
        public Employee(int empId, string empName, string department, string designation, double basicSalary, float serviceCompleted)
        {
            EmpId = empId;
            EmpName = empName;
            Department = department;
            Designation = designation;
            BasicSalary = basicSalary;
            ServiceCompleted = serviceCompleted;
            CompanyName = string.Empty;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Employee()
        {
            EmpId = 0;
            EmpName = string.Empty;
            Department = string.Empty;
            Designation = string.Empty;
            BasicSalary = 0;
            ServiceCompleted = 0;
            CompanyName = string.Empty;
        }

        /// <summary>
        /// Print details of the employee
        /// </summary>
        public void PrintEmployeeDetails()
        {
            Console.WriteLine("Employee ID : " +  EmpId);
            Console.WriteLine("Employee Name : " +  EmpName);
            Console.WriteLine("Employee Department : " +  Department);
            Console.WriteLine("Employee Designation : " +  Designation);
            Console.WriteLine("Employee Basic Salary : " +  BasicSalary);
            Console.WriteLine("Employee Service Completed : " + ServiceCompleted);
            Console.WriteLine("Employee Company Name: " +  CompanyName);
            Console.WriteLine("----------------------------\n");
        }

        public abstract double EmployeePF();


        public abstract string LeaveDetails();


        public abstract double GratuityAmount();
        
    }
}

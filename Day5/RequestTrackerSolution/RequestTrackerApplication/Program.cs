using RequestTrackerModelLibrary;

namespace RequestTrackerApplication
{
    internal class Program
    {
        Employee[] employees = new Employee[3];

        /// <summary>
        /// Prints the menu for performing the various operations
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Print Employees");
            Console.WriteLine("3. Search Employee by ID");
            Console.WriteLine("4. Update Employee by ID");
            Console.WriteLine("5. Delete Employee by ID");
            Console.WriteLine("0. Exit");
        }

        /// <summary>
        /// Takes the user's choice to do the appropriate action
        /// </summary>
        void EmployeeInteraction()
        {
            int choice;
            do
            {
                PrintMenu();
                Console.WriteLine("Please select an option");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Bye.....");
                        break;
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        PrintAllEmployees();
                        break;
                    case 3:
                        SearchAndPrintEmployee();
                        break;
                    case 4:
                        UpdateAndPrintEmployee();
                        break;
                    case 5:
                        DeleteAndPrintEmployee();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again");
                        break;
                }
            } while (choice != 0);
        }

        /// <summary>
        /// Add a set of new Employees and their data
        /// </summary>
        void AddEmployee()
        {
            if (employees[employees.Length - 1] != null)
            {
                Console.WriteLine("Sorry we have reached the maximum number of employees");
                return;
            }
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] == null)
                {
                    employees[i] = CreateEmployee(i);
                    Company company = new Company();
                    company.EmployeeClientVisit(employees[i], employees[i]);
                }
            }

        }

        /// <summary>
        /// Prints the details of all the employees
        /// </summary>
        void PrintAllEmployees()
        {
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] != null)
                    PrintEmployee(employees[i]);
            }
        }

        /// <summary>
        /// Creates a new employee object
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        static Employee CreateEmployee(int id)
        {
            Employee employee = new Employee();
            Console.WriteLine("Please enter the type of employee");
            string type = Console.ReadLine();
            if (type == "Permanent")
                employee = new PermanentEmployee();
            else if (type == "Contract")
                employee = new ContractEmployee();
            employee.Id = 101 + id;
            employee.BuildEmployeeFromConsole();
            return employee;
        }

        /// <summary>
        /// Prints the details of one employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        void PrintEmployee(Employee employee)
        {
            Console.WriteLine("---------------------------");
            employee.PrintEmployeeDetails();
            Console.WriteLine("---------------------------");
        }

        /// <summary>
        /// Gets the employee ID from user using console
        /// </summary>
        /// <returns></returns>
        int GetIdFromConsole()
        {
            int id = 0;
            Console.WriteLine("Please enter the employee Id");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid entry. Please try again");
            }
            return id;
        }

        /// <summary>
        /// Searches for a particular employee and prints their details
        /// </summary>
        void SearchAndPrintEmployee()
        {
            Console.WriteLine("Enter employee ID:");
            int id = GetIdFromConsole();
            Employee employee = SearchEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("No such Employee is present");
                return;
            }
            PrintEmployee(employee);
        }

        /// <summary>
        /// Finds the employee object given its ID
        /// </summary>
        /// <param name="id">ID of the employee to be searched</param>
        /// <returns></returns>
        Employee SearchEmployeeById(int id)
        {
            Employee employee = null;
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] != null && employees[i].Id == id)
                {
                    employee = employees[i];
                    break;
                }
            }
            return employee;
        }

        /// <summary>
        /// Updates the employee details and prints it
        /// </summary>
        void UpdateAndPrintEmployee()
        {
            Console.WriteLine("Enter employee ID:");
            int id = GetIdFromConsole();
            Employee employee = SearchEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("No such Employee is present");
                return;
            }
            UpdateMenu();
            int updateChoice = Convert.ToInt32(Console.ReadLine());
            switch (updateChoice)
            {
                case 1:
                    Console.WriteLine("Enter the name to be updated:");
                    employee.Name = Console.ReadLine() ?? "";
                    break;
                case 2:
                    Console.WriteLine("Enter the Date Of Birth to be updated:");
                    employee.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Enter the Salary to be updated:");
                    employee.Salary = Convert.ToDouble(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Employee Details after Updating:");
            Console.WriteLine("--------------------------------");
            PrintEmployee(employee);
        }

        /// <summary>
        /// Deletes an employee using their ID
        /// </summary>
        void DeleteAndPrintEmployee()
        {
            Console.WriteLine("Enter employee ID:");
            int id = GetIdFromConsole();
            Employee employee = SearchEmployeeById(id);
            if (employee == null)
            {
                Console.WriteLine("No such Employee is present");
                return;
            }
            for (int i = 0; i < employees.Length; i++)
            {
                if (employee.Id == employees[i].Id)
                {
                    employees[i] = null;
                }
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Employee Details after Deleting:");
            Console.WriteLine("--------------------------------");
            PrintAllEmployees();
        }

        /// <summary>
        /// Prints the choice menu for updating details
        /// </summary>
        void UpdateMenu()
        {
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Date of Birth");
            Console.WriteLine("3. Salary");
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.EmployeeInteraction();

            //Employee employee = new PermanentEmployee();
            //employee.BuildEmployeeFromConsole();
            //Console.WriteLine(employee);

        }

    }
}

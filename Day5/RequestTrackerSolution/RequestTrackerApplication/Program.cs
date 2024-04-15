using RequestTrackerModelLibrary;

namespace RequestTrackerApplication
{
    internal class Program
    {
        Employee[] employees;

        public Program()
        {
            employees = new Employee[3];
        }

        void AddEmployee()
        {
            if (employees[employees.Length - 1] != null)
            {
                Console.WriteLine("You cannot add new employee!");
                return;
            }
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] == null)
                {
                    employees[i] = CreateEmployee(i);
                }
            }
        }

        void PrintAllEmployees()
        {
            if (employees[0] == null)
            {
                Console.WriteLine("No Employees");
            }
            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i] != null)
                {
                    employees[i].PrintEmployeeDetails();
                }
            }
        }

        Employee SearchEmployeeById(int id)
        {
            Employee? employee = null;

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

        int GetIdFromConsole()
        {
            Console.WriteLine("Enter the ID: ");
            int id = 0;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter valid Id");
            }
            return id;
        }

        void SearchEmployee()
        {
            int id = GetIdFromConsole();
            Employee employee = SearchEmployeeById(id);
            if (employee != null)
            {
                employee.PrintEmployeeDetails();
            }
            else
            {
                Console.WriteLine("No Employee Found!");
            }
        }

        private Employee CreateEmployee(int id)
        {
            Employee employee = new Employee();
            employee.Id = 101 + id;
            employee.BuildEmployeeFromConsole();
            return employee;
        }

        void PrintMenu()
        {
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Display Employees");
            Console.WriteLine("3. Search Employees");
            Console.WriteLine("4. Exit");
        }

        void EmployeeInteraction()
        {
            int choice = 0;

            do
            {
                PrintMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        PrintAllEmployees();
                        break;
                    case 3:
                        SearchEmployee();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 4);
        }

        static void Main(string[] args)
        {
            
            Program program = new Program();
            //program.CreateEmployee();
            program.EmployeeInteraction();
            


        }
    }
}

using RequestTrackerBLLibrary;
//using RequestTrackerModelLibrary;
using RequestTrackerDALLibrary.Model;

namespace RequestTrackerApp
{
    internal class Program
    {
        private DepartmentBL _departmentService;
        private EmployeeBL _employeeService;

        public Program()
        {
            _departmentService = new DepartmentBL();
            _employeeService = new EmployeeBL();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Manage Departments");
            Console.WriteLine("2. Manage Employees");
            Console.WriteLine("3. Display Reports");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
        }

        private void ManageDepartments()
        {
            while (true)
            {
                Console.WriteLine("\nManage Departments:");
                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Change Department Name");
                Console.WriteLine("3. Get Department By ID");
                Console.WriteLine("4. Get Department By Name");
                Console.WriteLine("5. Delete Department");
                Console.WriteLine("6. View All Departments");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddDepartment();
                        break;
                    case "2":
                        ChangeDepartmentName();
                        break;
                    case "3":
                        GetDepartmentById();
                        break;
                    case "4":
                        GetDepartmentByName();
                        break;
                    case "5":
                        DeleteDepartment(); 
                        break;
                    case "6":
                        ViewAllDepartments();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllDepartments()
        {
            try
            {
                var departments = _departmentService.GetDepartmentList();
                Console.WriteLine("\nAll Departments:");
                foreach (var department in departments)
                {
                    Console.WriteLine(department);
                }
            }
            catch (DepartmentNotFoundException)
            {
                Console.WriteLine("No departments found.");
            }
        }

        private void ManageEmployees()
        {
            while (true)
            {
                Console.WriteLine("\nManage Employees:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Get Employee By ID");
                Console.WriteLine("3. Get Employee By Name");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. View All Employees");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        GetEmployeeById();
                        break;
                    case "3":
                        GetEmployeeByName();
                        break;
                    case "4":
                        UpdateEmployee();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "6":
                        ViewAllEmployees();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewAllEmployees()
        {
            try
            {
                var employees = _employeeService.GetEmployeeList();
                Console.WriteLine("\nAll Employees:");
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }
            catch (EmployeeNotFoundException)
            {
                Console.WriteLine("No Employees found.");
            }
        }

        private void DisplayReports()
        {
            Console.WriteLine("\nDisplaying reports:");
            Console.WriteLine("1. List all departments");
            Console.WriteLine("2. List all employees");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ListAllDepartments();
                    break;
                case "2":
                    ListAllEmployees();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        private void ListAllDepartments()
        {
            Console.WriteLine("\nAll Departments:");
            var departments = _departmentService.GetDepartmentList();
            foreach (var department in departments)
            {
                Console.WriteLine(department);
            }
        }

        private void ListAllEmployees()
        {
            Console.WriteLine("\nAll Employees:");
            var employees = _employeeService.GetEmployeeList();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

        private void AddDepartment()
        {
            try
            {
                Console.Write("Enter Department Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Department Head ID: ");
                int headId = int.Parse(Console.ReadLine()!);

                Department department = new Department
                {
                    Name = name,
                    DepartmentHead = headId
                };

                int? departmentId = _departmentService.AddDepartment(department);
                Console.WriteLine($"Department added with ID: {departmentId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the department: {e.Message}");
            }
        }

        private void ChangeDepartmentName()
        {
            try
            {
                Console.Write("Enter the old department name: ");
                string oldName = Console.ReadLine()!;
                Console.Write("Enter the new department name: ");
                string newName = Console.ReadLine()!;

                Department department = _departmentService.ChangeDepartmentName(oldName, newName);
                Console.WriteLine($"Department name changed to: {department.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while changing the department name: {e.Message}");
            }
        }

        private void GetDepartmentById()
        {
            try
            {
                Console.Write("Enter department ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Department department = _departmentService.GetDepartmentById(id);
                if (department != null)
                {
                    Console.WriteLine($"Department ID: {department.Id}, Name: {department.Name}, Head ID: {department.DepartmentHead}");
                }
                else
                {
                    Console.WriteLine("Department not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the department: {e.Message}");
            }
        }

        private void GetDepartmentByName()
        {
            try
            {
                Console.Write("Enter department name: ");
                string name = Console.ReadLine()!;

                Department department = _departmentService.GetDepartmentByName(name);
                if (department != null)
                {
                    Console.WriteLine($"Department Name: {department.Name}, ID: {department.Id}, Head ID: {department.DepartmentHead}");
                }
                else
                {
                    Console.WriteLine("Department not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the department: {e.Message}");
            }
        }

        private void AddEmployee()
        {
            try
            {
                //Console.Write("Enter Employee ID: ");
                //int id = int.Parse(Console.ReadLine()!);
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                DateTime dob = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Enter Salary: ");
                double salary = double.Parse(Console.ReadLine()!);
                Console.Write("Enter Employee Role: ");
                string role = Console.ReadLine()!;
                Console.Write("Enter Department ID: ");
                int departmentId = int.Parse(Console.ReadLine()!);

                //Console.WriteLine("ans" + _departmentService.GetDepartmentById(departmentId));
                Department employeeDepartment = _departmentService.GetDepartmentById(departmentId);
                Employee employee = new Employee
                {
                    //Id = id,
                    Name = name,
                    Dob = dob,
                    Salary = salary,
                    Role = role,
                    EmployeeDepartment = departmentId
                };
                int? employeeId = _employeeService.AddEmployee(employee);
                Console.WriteLine($"Employee added with ID: {employeeId}");
            }
            catch (Exception e)
            {

                Console.WriteLine($"An error occurred while adding the employee: {e.Message}");
            }
        }

        private void GetEmployeeById()
        {
            try
            {
                Console.Write("Enter Employee ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Employee employee = _employeeService.GetEmployeeById(id);
                if (employee != null)
                {
                    Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Role}, Department: {employee.EmployeeDepartment}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the employee: {e.Message}");
            }
        }

        private void GetEmployeeByName()
        {
            try
            {
                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine()!;

                Employee employee = _employeeService.GetEmployeeByName(name);
                if (employee != null)
                {
                    Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Role}, Department: {employee.EmployeeDepartment}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the employee: {e.Message}");
            }
        }

        private void UpdateEmployee()
        {
            try
            {
                Console.Write("Enter Employee ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var employee = _employeeService.GetEmployeeById(id);

                if (employee != null)
                {
                    Console.WriteLine("Current Employee Details:");
                    Console.WriteLine(employee);

                    Console.Write("Enter new Employee Name (leave blank to keep current): ");
                    string? newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        employee.Name = newName;
                    }

                    Console.Write("Enter new Date of Birth (yyyy-mm-dd, leave blank to keep current): ");
                    string? dobInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(dobInput))
                    {
                        employee.Dob = DateTime.Parse(dobInput);
                    }

                    Console.Write("Enter new Salary (leave blank to keep current): ");
                    string? salaryInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(salaryInput))
                    {
                        employee.Salary = double.Parse(salaryInput);
                    }

                    Console.Write("Enter new Role (leave blank to keep current): ");
                    string? newRole = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newRole))
                    {
                        employee.Role = newRole;
                    }

                    Console.Write("Enter new Department ID (leave blank to keep current): ");
                    string? departmentIdInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(departmentIdInput))
                    {
                        int departmentId = int.Parse(departmentIdInput);
                        Department dept = _departmentService.GetDepartmentById(departmentId);
                        employee.EmployeeDepartment = dept.Id;
                    }

                    _employeeService.UpdateEmployee(employee);
                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while updating the employee: {e.Message}");
            }
        }

        private void DeleteEmployee()
        {
            try
            {
                Console.Write("Enter Employee ID: ");
                int id = int.Parse(Console.ReadLine()!);

                bool success = _employeeService.DeleteEmployee(id);
                if (success)
                {
                    Console.WriteLine("Employee deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting the employee: {e.Message}");
            }
        }

        private void DeleteDepartment()
        {
            try
            {
                Console.Write("Enter Department ID: ");
                int id = int.Parse(Console.ReadLine()!);

                bool success = _departmentService.DeleteDepartment(id);
                if (success)
                {
                    Console.WriteLine("Department deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Department with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting the Department: {e.Message}");
            }
        }

        private void Run()
        {
            while (true)
            {
                DisplayMenu();

                switch (Console.ReadLine())
                {
                    case "1":
                        ManageDepartments();
                        break;
                    case "2":
                        ManageEmployees();
                        break;
                    case "3":
                        DisplayReports();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using RequestTrackerBLLibrary;
using RequestTrackerModelLibrary;
using System;
using System.Threading.Tasks;

namespace RequestTrackerFEAPP
{
    internal class Program
    {
        private static Employee? CurrentLoggedInEmployee;
        private readonly IEmployeeLoginBL employeeLoginBL;
        private readonly IRequestBL requestBL;

        public Program()
        {
            employeeLoginBL = new EmployeeLoginBL();
            requestBL = new RequestBL();
            CurrentLoggedInEmployee = null;
        }

        static async Task Main(string[] args)
        {
            Program program = new Program();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Request Tracker");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await program.GetLoginDetails();
                        break;
                    case "2":
                        await program.GetRegisterDetails();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        async Task GetLoginDetails()
        {
            await Console.Out.WriteLineAsync("Please enter Employee Id");
            int id = Convert.ToInt32(Console.ReadLine());
            await Console.Out.WriteLineAsync("Please enter your password");
            string? password = Console.ReadLine() ?? "";
            await EmployeeLoginAsync(id, password);
        }

        async Task GetRegisterDetails()
        {
            await Console.Out.WriteLineAsync("Please enter Employee Id");
            int id = Convert.ToInt32(Console.ReadLine());
            await Console.Out.WriteLineAsync("Please enter your password");
            string? password = Console.ReadLine() ?? "";
            await EmployeeRegisterAsync(id, password);
        }

        async Task EmployeeLoginAsync(int username, string password)
        {
            Employee employee = new Employee() { Password = password, Id = username };
            var result = await employeeLoginBL.Login(employee);
            if (result)
            {
                CurrentLoggedInEmployee = employee;
                await Console.Out.WriteLineAsync("Login Success");

                await Task.Delay(3000);

                await ManageRequests();
            }
            else
            {
                Console.Out.WriteLine("Invalid username or password");
            }
        }

        async Task EmployeeRegisterAsync(int username, string password)
        {
            Employee employee = new Employee() { Password = password, Id = username, Role = "User" };
            var result = await employeeLoginBL.Register(employee);
            if (result != null)
            {
                await Console.Out.WriteLineAsync("Registration Success with ID " + result.Id);
            }
            else
            {
                Console.Out.WriteLine("Registration Failed");
            }
        }

        async Task ManageRequests()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Request Management Menu");
                Console.WriteLine("1. Add Request");
                Console.WriteLine("2. Update Request");
                Console.WriteLine("3. Close Request");
                Console.WriteLine("4. View Request Details");
                Console.WriteLine("5. View All Requests");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddRequest();
                        break;
                    case "2":
                        await UpdateRequest();
                        break;
                    case "3":
                        await CloseRequest();
                        break;
                    case "4":
                        await ViewRequestDetails();
                        break;
                    case "5":
                        await ViewAllRequests();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private async Task CloseRequest()
        {
            Console.WriteLine("Enter Request Number to be Closed: ");
            int requestNumber = Convert.ToInt32(Console.ReadLine());
            var requestToUpdate = await requestBL.GetRequestById(requestNumber);
            if (requestToUpdate == null)
            {
                Console.WriteLine("Request not found!");
                return;
            }

            if (requestToUpdate.RequestStatus == "Closed")
            {
                Console.WriteLine("Request Closed Already!");
                return;
            }

            if (CurrentLoggedInEmployee != null)
            {
                await requestBL.CloseRequest(requestNumber, CurrentLoggedInEmployee);
                Console.WriteLine("Request Closed Successfully!");
            }
            else
            {
                await Console.Out.WriteLineAsync("Log in Failed");
            }
        }

        async Task AddRequest()
        {
            Console.WriteLine("Enter Request Details:");
            Console.Write("Request Message: ");
            string? requestMessage = Console.ReadLine() ?? "";
            DateTime currentDateTime = DateTime.Now;
            DateTime requestDate = currentDateTime;
            Request request = null;
            if (CurrentLoggedInEmployee != null)
            {
                request = new Request
                {
                    RequestMessage = requestMessage,
                    RequestDate = requestDate,
                    RequestStatus = "Opened",
                    RequestRaisedBy = CurrentLoggedInEmployee.Id,
                };
            }
            if (request != null)
            {
                await requestBL.OpenRequest(request);
                Console.WriteLine("Request Added Successfully!");
            }
        }

        async Task UpdateRequest()
        {
            Console.WriteLine("Enter Request Number to Update:");
            int requestNumber = Convert.ToInt32(Console.ReadLine());
            var requestToUpdate = await requestBL.GetRequestById(requestNumber);
            if (requestToUpdate == null)
            {
                Console.WriteLine("Request not found!");
                return;
            }

            if(requestToUpdate.RequestStatus == "Closed")
            {
                Console.WriteLine("Request Closed Already!");
                return;
            }

            Console.Write("Request Status: ");
            string? requestStatus = Console.ReadLine() ?? "";

            await requestBL.UpdateRequest(requestNumber, requestStatus);
            Console.WriteLine("Request Updated Successfully!");
        }



        async Task ViewRequestDetails()
        {
            Console.WriteLine("Enter Request Number to View Details:");
            int requestNumber = Convert.ToInt32(Console.ReadLine());
            var requestDetails = await requestBL.GetRequestById(requestNumber);
            if (requestDetails == null)
            {
                Console.WriteLine("Request not found!");
                return;
            }

            Console.WriteLine(requestDetails);
        }

        async Task ViewAllRequests()
        {
            var allRequests = await requestBL.GetAllRequests();
            if (allRequests.Count == 0)
            {
                Console.WriteLine("No requests found!");
                return;
            }

            Console.WriteLine("All Requests:");
            foreach (var request in allRequests)
            {
                Console.WriteLine(request);
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}
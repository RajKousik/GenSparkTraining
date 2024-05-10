using RequestTrackerBLLibrary;
using RequestTrackerDALLibrary;
using RequestTrackerModelLibrary;
using System;

namespace RequestTrackerFEAPP
{
    internal class Program
    {
        private static Employee? CurrentLoggedInEmployee;
        private readonly IEmployeeLoginBL employeeLoginBL;
        private readonly IRequestBL requestBL;
        private readonly IRequestSolutionBL solutionBL;
        private readonly ISolutionFeedbackBL feedbackBL;

        public Program()
        {
            this.employeeLoginBL = new EmployeeLoginBL();
            this.requestBL = new RequestBL();
            this.solutionBL = new RequestSolutionBL(requestBL);
            this.feedbackBL = new SolutionFeedbackBL();
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
            Console.Clear();
            Console.WriteLine("-----Login-----");
            await Console.Out.WriteLineAsync("Please enter Employee Id");
            int id = Convert.ToInt32(Console.ReadLine());
            await Console.Out.WriteLineAsync("Please enter your password");
            string? password = Console.ReadLine() ?? "";
            await EmployeeLoginAsync(id, password);
        }

        async Task GetRegisterDetails()
        {
            Console.Clear();
            Console.WriteLine("-----Register-----");
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

            if (result != null)
            {
                CurrentLoggedInEmployee = result;
                await Console.Out.WriteLineAsync("Login Success");
                if (CurrentLoggedInEmployee.Role == "Admin")
                    await ManageAdminRequests();
                else
                    await ManageUserRequests();
            }
            else
            {
                Console.Out.WriteLine("Invalid username or password");
            }
        }

        async Task EmployeeRegisterAsync(int username, string password)
        {
            string role = "User";
            if (CurrentLoggedInEmployee != null && CurrentLoggedInEmployee.Role == "Admin")
                role = "Admin";

            Employee employee = new Employee() { Password = password, Id = username, Role = role };
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

        async Task ManageUserRequests()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("User Request Management Menu");
                Console.WriteLine("1. Raise Request");
                Console.WriteLine("2. View Request Status");
                Console.WriteLine("3. View Solutions");
                Console.WriteLine("4. Give Feedback");
                Console.WriteLine("5. Respond to Solution");
                Console.WriteLine("6. Logout");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddRequest();
                        break;
                    case "2":
                        await ViewRequestDetails();
                        break;
                    case "3":
                        await ViewSolutions();
                        break;
                    case "4":
                        await GiveFeedback();
                        break;
                    case "5":
                        await RespondToSolution();
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

        async Task ManageAdminRequests()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Admin Request Management Menu");
                Console.WriteLine("1. Raise Request");
                Console.WriteLine("2. View Request Status (All Requests)");
                Console.WriteLine("3. View Solutions (All Solutions)");
                Console.WriteLine("4. Give Feedback (Only for requests raised by them)");
                Console.WriteLine("5. Respond to Solution (Only for requests raised by them)");
                Console.WriteLine("6. Provide Solution");
                Console.WriteLine("7. Mark Request as Closed");
                Console.WriteLine("8. View Feedbacks (Only feedbacks given to them)");
                Console.WriteLine("9. Logout");
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddRequest();
                        break;
                    case "2":
                        await ViewAllRequests();
                        break;
                    case "3":
                        await ViewAllSolutions();
                        break;
                    case "4":
                        await GiveFeedback();
                        break;
                    case "5":
                        await RespondToSolution();
                        break;
                    case "6":
                        await ProvideSolution();
                        break;
                    case "7":
                        await MarkRequestAsClosed();
                        break;
                    case "8":
                        await ViewFeedbacks();
                        break;
                    case "9":
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

        async Task AddRequest()
        {
            Console.Write("Request Message: ");
            string? requestMessage = Console.ReadLine() ?? "";
            DateTime currentDateTime = DateTime.Now;
            DateTime requestDate = currentDateTime;

            Request request = new Request
            {
                RequestMessage = requestMessage,
                RequestDate = requestDate,
                RequestStatus = "Opened",
                RequestRaisedBy = CurrentLoggedInEmployee.Id,
                RequestClosedBy = CurrentLoggedInEmployee?.Id ?? 0
            };

            var Addrequest = await requestBL.OpenRequest(request);
            Console.WriteLine("Request Added Successfully with ID : " + Addrequest);
        }

        async Task ViewRequestDetails()
        {
            var requestDetails = await requestBL.GetAllRequestsById(CurrentLoggedInEmployee.Id);
            if (requestDetails == null)
            {
                Console.WriteLine("Request not found!");
                return;
            }
            foreach (var reqdet in requestDetails)
            {
                Console.WriteLine("\n---------------------------------------");
                Console.WriteLine($"Request Number: {reqdet.RequestNumber}");
                Console.WriteLine($"Request Message: {reqdet.RequestMessage}");
                Console.WriteLine($"Request Date: {reqdet.RequestDate}");
                Console.WriteLine($"Closed Date: {reqdet.ClosedDate}");
                Console.WriteLine($"Request Status: {reqdet.RequestStatus}");
                Console.WriteLine($"Raised By Employee Id: {reqdet.RequestRaisedBy}");
                Console.WriteLine($"Closed By Employee Id: {reqdet.RequestClosedBy}");
                Console.WriteLine("---------------------------------------\n");
            }
        }

        async Task ViewSolutions()
        {
            Console.WriteLine("Enter Request Number to View Solutions:");
            int requestNumber = Convert.ToInt32(Console.ReadLine());
            var solution = await solutionBL.GetAllUserRequestSolutions(CurrentLoggedInEmployee.Id, requestNumber);
            foreach (var sol in solution)
            {
                if (sol.RequestId == requestNumber)
                {
                    Console.WriteLine($"Solution ID: {sol.SolutionId}, Description: {sol.SolutionDescription}, Comments: {sol.RequestRaiserComment}");
                }
            }
            if (solution == null)
            {
                Console.WriteLine("No solution found for this request.");
                return;
            }

            //Console.WriteLine($"Solution ID: {solution.SolutionId}, Description: {solution.SolutionDescription}");
        }

        async Task GiveFeedback()
        {
            //Console.WriteLine("Enter the solution ID to give feedback:");
            //int solutionId = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Enter your rating (out of 5): ");
            //float rating = float.Parse(Console.ReadLine() ?? "0");
            //Console.WriteLine("Enter your remarks: ");
            //string remarks = Console.ReadLine() ?? "";

            //var feedback = new SolutionFeedback
            //{
            //    Rating = rating,
            //    Remarks = remarks,
            //    SolutionId = solutionId,
            //    FeedbackBy = CurrentLoggedInEmployee?.Id ?? 0,
            //    FeedbackDate = DateTime.Now
            //};

            //await feedbackBL.AddFeedback(feedback);
            //Console.WriteLine("Feedback submitted successfully.");
        }

        async Task RespondToSolution()
        {
            Console.WriteLine("Enter the solution ID to respond:");
            int solutionId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your response: ");
            string response = Console.ReadLine() ?? "";

            var solution = await solutionBL.RespondToSolution(solutionId, response);
            if (solution == null)
            {
                Console.WriteLine("Solution not found.");
                return;
            }

            Console.WriteLine("Response submitted successfully.");
        }

        async Task ProvideSolution()
        {
            Console.WriteLine("Enter Request Number to Provide Solution:");
            int requestNumber = Convert.ToInt32(Console.ReadLine());
            var res = await requestBL.GetRequestById(requestNumber);
            if (res.RequestStatus == "Closed")
            {
                Console.WriteLine("Request is already closed.");
                return;
            }
            Console.WriteLine("Enter Solution Description:");
            string solutionDescription = Console.ReadLine() ?? "";

            var solution = new RequestSolution
            {
                SolutionDescription = solutionDescription,
                RequestId = requestNumber,
                SolvedBy = CurrentLoggedInEmployee?.Id ?? 0,
            };

            await solutionBL.AddRequestSolution(solution);
            Console.WriteLine("Solution provided successfully.");
        }

        async Task MarkRequestAsClosed()
        {
            //Console.WriteLine("Enter Request Number to Mark as Closed:");
            //int requestNumber = Convert.ToInt32(Console.ReadLine());
            //var request = await requestBL.GetRequestById(requestNumber);

            ////close last solution
            //var solution = await solutionBL.GetAllSolutions();
            //solution[^1].IsSolved = true;
            //await solutionBL.UpdateSolution(solution[^1]);
            //if (solution == null)
            //{
            //    Console.WriteLine("No solution found for this request.");
            //    return;
            //}
            ////close req
            //if (request == null)
            //{
            //    Console.WriteLine("Request not found.");
            //    return;
            //}

            //request.RequestStatus = "Closed";
            //request.ClosedDate = DateTime.Now;
            //await requestBL.UpdateRequest(request, request.RequestStatus);
            //Console.WriteLine("Request marked as closed successfully.");

            //To Be Implemented
        }

        async Task ViewFeedbacks()
        {
            //Console.WriteLine("Enter Request Number to View Feedbacks:");
            //int requestNumber = Convert.ToInt32(Console.ReadLine());

            //var feedback = await feedbackBL.GetFeedback(requestNumber);
            //if (feedback == null)
            //{
            //    Console.WriteLine("No feedback found for this request.");
            //    return;
            //}

            //Console.WriteLine($"Feedback ID: {feedback.FeedbackId}, Rating: {feedback.Rating}, Remarks: {feedback.Remarks}");
        }


        async Task ViewAllRequests()
        {
            var allRequests = await requestBL.GetAllRequests();
            if (allRequests == null || allRequests.Count == 0)
            {
                Console.WriteLine("No requests found.");
                return;
            }
            foreach (var request in allRequests)
            {
                Console.WriteLine($"Request Number: {request.RequestNumber}, Message: {request.RequestMessage}, Status: {request.RequestStatus}");
            }
        }

        async Task ViewAllSolutions()
        {
            var allSolutions = await solutionBL.GetAllAdminRequestSolutions();
            if (allSolutions == null || allSolutions.Count == 0)
            {
                Console.WriteLine("No solutions found.");
                return;
            }
            foreach (var solution in allSolutions)
            {
                Console.WriteLine($"Solution ID: {solution.SolutionId}, Description: {solution.SolutionDescription}");
            }
        }

    }
}
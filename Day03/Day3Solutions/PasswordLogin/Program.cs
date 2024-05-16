namespace PasswordLogin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int attempts = 0;
            const int maxAttempts = 3;
            const string correctUsername = "ABC";
            const string correctPassword = "123";

            while (attempts < maxAttempts)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine()??"";

                Console.Write("Enter password: ");
                string password = Console.ReadLine()??"";

                if (username == correctUsername && password == correctPassword)
                {
                    Console.WriteLine("Login successful! Welcome User");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                    attempts++;
                }
            }

            if (attempts >= maxAttempts)
            {
                Console.WriteLine("You have exceeded the number of attempts..!");
            }

            Console.ReadLine();
        }
    }
}

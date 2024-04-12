using DoctorApplication.Models;

namespace DoctorApplication
{
    internal class Program
    {

        static Doctor CreateNewDoctor()
        {
            Console.WriteLine("Enter the details of the new doctor:");
            Console.WriteLine("ID:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID:");
            }

            Console.WriteLine("Name:");
            string? name = Console.ReadLine();

            Console.WriteLine("Age:");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Invalid input. Please enter a valid age:");
            }

            Console.WriteLine("Experience:");
            int experience;
            while (!int.TryParse(Console.ReadLine(), out experience))
            {
                Console.WriteLine("Invalid input. Please enter a valid experience:");
            }

            Console.WriteLine("Degree:");
            string? degree = Console.ReadLine();

            Console.WriteLine("Specialty:");
            string? specialty = Console.ReadLine();

            Doctor newDoctor = new Doctor(id, name!, age, experience, degree!, specialty!);
            return newDoctor;
        }

        static void Main(string[] args)
        {
            int CurrentDoctorCount = 4;
            Doctor[] doctors = new Doctor[CurrentDoctorCount];
            doctors[0] = new Doctor(1, "Dr. Smith", 45, 20, "MD", "Cardiology");
            doctors[1] = new Doctor(2, "Dr. Jones", 50, 25, "PhD", "Neurology");
            doctors[2] = new Doctor(3, "Dr. Patel", 40, 15, "MBBS", "Pediatrics");
            doctors[3] = new Doctor(3, "Dr. Raj Kousik", 45, 10, "MD", "Cardiology");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. View all doctors");
                Console.WriteLine("2. Search doctors by specialty");
                Console.WriteLine("3. Add new doctor");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Enter your choice:");
                int? choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("All Doctors:");
                        foreach (var doctor in doctors)
                        {
                            doctor.PrintDoctorDetails();
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter a specialty to search for doctors:");
                        string? searchSpecialty = Console.ReadLine();

                        Console.WriteLine($"Doctors with specialty {searchSpecialty}:");
                        foreach (var doctor in doctors)
                        {
                            if (doctor.Speciality.Equals(searchSpecialty, StringComparison.OrdinalIgnoreCase))
                            {
                                doctor.PrintDoctorDetails();
                            }
                        }
                        break;
                    case 3:
                        Doctor newDoctor = CreateNewDoctor();
                        Array.Resize(ref doctors, doctors.Length + 1);
                        doctors[doctors.Length - 1] = newDoctor;

                        Console.WriteLine("New doctor added successfully!");
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            }

        }
    }
}

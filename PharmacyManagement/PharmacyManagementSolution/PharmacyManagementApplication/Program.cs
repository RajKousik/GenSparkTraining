using PharmacyManagementBLLibrary;
using PharmacyManagementModelLibrary;

namespace PharmacyManagementApplication
{
    internal class Program
    {
        private PatientBL _patientService; // New addition
        private DoctorBL _doctorService; 
        private PrescriptionBL _prescriptionService; 
        private SaleBL _saleService; 
        private DrugBL _drugService; 

        public Program()
        {
            _patientService = new PatientBL(); // New addition
            _doctorService = new DoctorBL(); // New addition
            _prescriptionService = new PrescriptionBL(); // New addition
            _saleService = new SaleBL(); // New addition
            _drugService = new DrugBL(); // New addition
        }
        private void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Manage Patients");
            Console.WriteLine("2. Manage Doctors");
            Console.WriteLine("3. Manage Prescriptions");
            Console.WriteLine("4. Manage Drugs");
            Console.WriteLine("5. Manage Sales");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
        }
        private void Run()
        {
            while (true)
            {
                DisplayMenu();

                switch (Console.ReadLine())
                {
                    case "1":
                        ManagePatients();
                        break;
                    case "2":
                        //ManageDoctors();
                        break;
                    case "3":
                        //ManagePrescription(); // New addition
                        break;
                    case "4":
                        //ManageDrugs();
                        break;
                    case "5":
                        //ManageSales();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ManagePatients() // New addition
        {
            while (true)
            {
                Console.WriteLine("\nManage Patients:");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Get Patient By ID");
                Console.WriteLine("3. Get Patient By Name");
                Console.WriteLine("4. Delete Patient");
                Console.WriteLine("5. View All Patients");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddPatient();
                        break;
                    case "2":
                        GetPatientById();
                        break;
                    case "3":
                        GetPatientByName();
                        break;
                    case "4":
                        DeletePatient();
                        break;
                    case "5":
                        ViewAllPatients();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddPatient() // New addition
        {
            try
            {
                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                DateTime dob = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Contact: ");
                string contact = Console.ReadLine();

                Patient patient = new Patient
                {
                    Name = name,
                    DateOfBirth = dob,
                    Contact = contact
                };

                int patientId = _patientService.AddPatient(patient);
                Console.WriteLine($"Patient added with ID: {patientId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the patient: {e.Message}");
            }
        }

        private void GetPatientById() // New addition
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int id = int.Parse(Console.ReadLine());

                Patient patient = _patientService.GetPatientById(id);
                if (patient != null)
                {
                    Console.WriteLine($"Patient ID: {patient.Id}, Name: {patient.Name}, DOB: {patient.DateOfBirth}, Contact: {patient.Contact}");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the patient: {e.Message}");
            }
        }

        private void GetPatientByName() // New addition
        {
            try
            {
                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine();

                Patient patient = _patientService.GetPatientByName(name);
                if (patient != null)
                {
                    Console.WriteLine($"Patient ID: {patient.Id}, Name: {patient.Name}, DOB: {patient.DateOfBirth}, Contact: {patient.Contact}");
                }
                else
                {
                    Console.WriteLine("Patient not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the patient: {e.Message}");
            }
        }

        private void DeletePatient() // New addition
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int id = int.Parse(Console.ReadLine());

                bool success = _patientService.DeletePatient(id);
                if (success)
                {
                    Console.WriteLine("Patient deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Patient with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting the patient: {e.Message}");
            }
        }

        private void ViewAllPatients() // New addition
        {
            try
            {
                var patients = _patientService.GetAllPatients();
                Console.WriteLine("\nAll Patients:");
                foreach (var patient in patients)
                {
                    Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, DOB: {patient.DateOfBirth}, Contact: {patient.Contact}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"No patients found {e.Message}");
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
    }
}

using PharmacyManagementBLLibrary;
using PharmacyManagementBLLibrary.Exceptions;
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
            _drugService = new DrugBL();
            _patientService = new PatientBL(); 
            _doctorService = new DoctorBL(); 
            _saleService = new SaleBL();
            _prescriptionService = new PrescriptionBL(_drugService, _saleService); // New addition

        }

        public static void ClearConsole()
        {
            Console.Clear();
        }
        private void DisplayMenu()
        {
            ClearConsole();
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
                        ManageDoctors();
                        break;
                    case "3":
                        ManagePrescriptions();
                        break;
                    case "4":
                        ManageDrugs();
                        break;
                    case "5":
                        ManageSales();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private void ManageSales()
        {
            while (true)
            {
                ClearConsole();
                Console.WriteLine("\nManage Sales:");
                Console.WriteLine("1. View Sale Receipt By ID");
                Console.WriteLine("2. View All Sales");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ViewSaleById();
                        break;
                    case "2":
                        ViewAllSales();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ViewSaleById()
        {
            try
            {
                Console.Write("Enter Sale ID: ");
                int saleId = int.Parse(Console.ReadLine()!);

                Sales sale = _saleService.GetSaleById(saleId);
                if (sale != null)
                {
                    Console.WriteLine($"Sale ID: {sale.SaleId}, Type: {sale.SalesType}, Total Price: {sale.TotalPrice}");
                }
                else
                {
                    Console.WriteLine("Sale not found.");
                }
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the sale: {e.Message}");
            }
        }

        private void ViewAllSales()
        {
            try
            {
                var sales = _saleService.GetAllSales();
                Console.WriteLine("\nAll Sales:");
                foreach (var sale in sales)
                {
                    Console.WriteLine($"Sale ID: {sale.SaleId}, Type: {sale.SalesType}, Total Price: {sale.TotalPrice}");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving all sales: {e.Message}");
            }
        }


        private void ManageDrugs()
        {
            while (true)
            {
                ClearConsole();
                Console.WriteLine("\nManage Drugs:");
                Console.WriteLine("1. Add Drug");
                Console.WriteLine("2. Update Drug");
                Console.WriteLine("3. Remove Drug");
                Console.WriteLine("4. View All Drugs");
                Console.WriteLine("4. Delete Expired Drugs");
                Console.WriteLine("5. Delete Out Of stock Drugs");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddDrug();
                        break;
                    case "2":
                        UpdateDrug();
                        break;
                    case "3":
                        RemoveDrug();
                        break;
                    case "4":
                        ViewAllDrugs();
                        break;
                    case "5":
                        DeleteExpiredDrugs();
                        break;
                    case "6":
                        DeleteOutOfStockDrugs();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void DeleteExpiredDrugs()
        {
            try
            {
                var expiredDrugs = _drugService.GetDrugList().Where(drug => drug.ExpiryDate <= DateTime.Today).ToList();
                foreach (var expiredDrug in expiredDrugs)
                {
                    _drugService.DeleteDrug(expiredDrug.Id);
                    Console.WriteLine($"Expired drug '{expiredDrug.Name}' removed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting expired drugs: {e.Message}");
            }
        }

        private void DeleteOutOfStockDrugs()
        {
            try
            {
                var outOfStockDrugs = _drugService.GetDrugList().Where(drug => drug.InStock == 0).ToList();
                foreach (var outOfStockDrug in outOfStockDrugs)
                {
                    _drugService.DeleteDrug(outOfStockDrug.Id);
                    Console.WriteLine($"Out of stock drug '{outOfStockDrug.Name}' removed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting out of stock drugs: {e.Message}");
            }
        }

        private void AddDrug()
        {
            try
            {
                Console.Write("Enter Drug Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Brand: ");
                string brand = Console.ReadLine()!;
                Console.Write("Enter Dosage Form: ");
                string dosageForm = Console.ReadLine()!;
                Console.Write("Enter Strength: ");
                string strength = Console.ReadLine()!;
                Console.Write("Enter Quantity in Stock: ");
                int inStock = int.Parse(Console.ReadLine()!);
                Console.Write("Enter Expiry Date (yyyy-mm-dd): ");
                DateTime expiryDate = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Enter Price: ");
                double price = double.Parse(Console.ReadLine()!);

                Drugs drug = new Drugs(name, brand, dosageForm, strength, inStock, expiryDate, price);
                int drugId = _drugService.AddDrug(drug);
                Console.WriteLine($"Drug added with ID: {drugId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the drug: {e.Message}");
            }
        }

        private void UpdateDrug()
        {
            try
            {
                Console.Write("Enter Drug ID: ");
                int drugId = int.Parse(Console.ReadLine()!);
                Drugs existingDrug = _drugService.GetDrugById(drugId);
                if (existingDrug == null)
                {
                    throw new DrugNotFoundException();
                }

                Console.Write("Enter New Brand (leave blank to keep current): ");
                string newBrand = Console.ReadLine();
                Console.Write("Enter New Dosage Form (leave blank to keep current): ");
                string newDosageForm = Console.ReadLine();
                Console.Write("Enter New Strength (leave blank to keep current): ");
                string newStrength = Console.ReadLine();
                Console.Write("Enter New Quantity in Stock (leave blank to keep current): ");
                string inStockInput = Console.ReadLine();
                int? newInStock = !string.IsNullOrWhiteSpace(inStockInput) ? int.Parse(inStockInput) : (int?)null;
                Console.Write("Enter New Expiry Date (yyyy-mm-dd, leave blank to keep current): ");
                string expiryDateInput = Console.ReadLine();
                DateTime? newExpiryDate = !string.IsNullOrWhiteSpace(expiryDateInput) ? DateTime.Parse(expiryDateInput) : (DateTime?)null;
                Console.Write("Enter New Price (leave blank to keep current): ");
                string priceInput = Console.ReadLine();
                double? newPrice = !string.IsNullOrWhiteSpace(priceInput) ? double.Parse(priceInput) : (double?)null;

                Drugs updatedDrug = new Drugs
                {
                    Id = drugId,
                    Name = existingDrug.Name,
                    Brand = newBrand ?? existingDrug.Brand,
                    DosageForm = newDosageForm ?? existingDrug.DosageForm,
                    Strength = newStrength ?? existingDrug.Strength,
                    InStock = newInStock ?? existingDrug.InStock,
                    ExpiryDate = newExpiryDate ?? existingDrug.ExpiryDate,
                    Price = newPrice ?? existingDrug.Price
                };

                _drugService.UpdateDrug(updatedDrug);
                Console.WriteLine($"Drug updated successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while updating the drug: {e.Message}");
            }
        }

        private void RemoveDrug()
        {
            try
            {
                Console.Write("Enter Drug ID: ");
                int drugId = int.Parse(Console.ReadLine()!);
                bool success = _drugService.DeleteDrug(drugId);
                if (success)
                {
                    Console.WriteLine("Drug removed successfully.");
                }
                else
                {
                    Console.WriteLine($"Drug with ID {drugId} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while removing the drug: {e.Message}");
            }
        }

        private void ViewAllDrugs()
        {
            try
            {
                var drugs = _drugService.GetDrugList();
                Console.WriteLine("\nAll Drugs:");
                foreach (var drug in drugs)
                {
                    Console.WriteLine(drug);
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (DrugNotFoundException)
            {
                Console.WriteLine("No drugs found.");
            }
        }


        private void ManagePrescriptions()
        {
            while (true)
            {
                Console.WriteLine("\nManage Prescriptions:");
                Console.WriteLine("1. Add Prescription");
                Console.WriteLine("2. Get Prescription By ID");
                Console.WriteLine("3. Get Prescriptions By Patient ID");
                Console.WriteLine("4. Get Prescriptions By Doctor ID");
                Console.WriteLine("5. View All Prescriptions");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddPrescription();
                        break;
                    case "2":
                        GetPrescriptionById();
                        break;
                    case "3":
                        GetPrescriptionsByPatientId();
                        break;
                    case "4":
                        GetPrescriptionsByDoctorId();
                        break;
                    case "5":
                        ViewAllPrescriptions();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddPrescription()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = int.Parse(Console.ReadLine()!);
                Patient patient = _patientService.GetPatientById(patientId);

                Console.Write("Enter Doctor ID: ");
                int doctorId = int.Parse(Console.ReadLine()!);
                Doctor doctor = _doctorService.GetDoctorById(doctorId);

                Console.Write("Enter Drug Names separated by commas: ");
                string drugNamesInput = Console.ReadLine()!;
                string[] drugNames = drugNamesInput.Split(',');

                List<Drugs> drugsList = new List<Drugs>();
                foreach (var drugName in drugNames)
                {
                    Drugs drug = _drugService.GetDrugByName(drugName.Trim());
                    if (drug == null)
                    {
                        throw new DrugNotFoundException();
                    }
                    drugsList.Add(drug);
                }

                Console.Write("Enter Dosage: ");
                int dosage = int.Parse(Console.ReadLine()!);

                // Create a prescription for each drug
                
                Prescription prescription = new Prescription(patient, doctor, drugsList, dosage);
                int prescriptionId = _prescriptionService.ProcessPrescription(prescription);

                Console.WriteLine($"Prescription added with ID: {prescriptionId}");
                Console.ReadKey();


            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the prescription: {e.Message}");
            }
        }

        private void GetPrescriptionById()
        {
            try
            {
                Console.Write("Enter Prescription ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Prescription prescription = _prescriptionService.GetPrescriptionById(id);
                if (prescription != null)
                {
                    Console.WriteLine($"Prescription ID: {prescription.Id}, Patient Name: {prescription.Patient.Name}, Doctor Name: {prescription.Doctor.Name}, Drug Name: {prescription.Drugs}, Dosage: {prescription.Dosage}");
                }
                else
                {
                    Console.WriteLine("Prescription not found.");
                }
                //Console.WriteLine("Press any key to continue");
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the prescription: {e.Message}");
            }
        }

        private void GetPrescriptionsByPatientId()
        {
            try
            {
                Console.Write("Enter Patient ID: ");
                int patientId = int.Parse(Console.ReadLine()!);

                var prescriptions = _prescriptionService.GetPrescriptionsByPatientId(patientId);
                Console.WriteLine("\nPrescriptions for Patient ID " + patientId + ":");
                foreach (var prescription in prescriptions)
                {
                    Console.WriteLine(prescription);
                }
                //Console.WriteLine("Press any key to continue");
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the prescriptions: {e.Message}");
            }
        }

        private void GetPrescriptionsByDoctorId()
        {
            try
            {
                Console.Write("Enter Doctor ID: ");
                int doctorId = int.Parse(Console.ReadLine()!);

                var prescriptions = _prescriptionService.GetPrescriptionsByDoctorId(doctorId);
                Console.WriteLine("\nPrescriptions for Doctor ID " + doctorId + ":");
                foreach (var prescription in prescriptions)
                {
                    Console.WriteLine(prescription);
                }
                //Console.WriteLine("Press any key to continue");
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the prescriptions: {e.Message}");
            }
        }

        private void ViewAllPrescriptions()
        {
            try
            {
                var prescriptions = _prescriptionService.GetAllPrescriptions();
                Console.WriteLine("\nAll Prescriptions:");
                foreach (var prescription in prescriptions)
                {
                    Console.WriteLine(prescription);
                }
                //Console.WriteLine("Press any key to continue");
                //Console.ReadKey();
            }
            catch (PrescriptionNotFoundException)
            {
                Console.WriteLine("No prescriptions found.");
            }
        }


        private void ManageDoctors()
        {
            
            while (true)
            {
                ClearConsole();
                Console.WriteLine("\nManage Doctors:");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Get Doctor By ID");
                Console.WriteLine("3. Get Doctor By Name");
                Console.WriteLine("4. Delete Doctor");
                Console.WriteLine("5. View All Doctors");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddDoctor();
                        break;
                    case "2":
                        GetDoctorById();
                        break;
                    case "3":
                        GetDoctorByName();
                        break;
                    case "4":
                        DeleteDoctor();
                        break;
                    case "5":
                        ViewAllDoctors();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void AddDoctor()
        {
            try
            {
                Console.Write("Enter Doctor Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Enter Doctor Specialization: ");
                string specialization = Console.ReadLine()!;
                Console.Write("Enter Doctor Contact: ");
                string contact = Console.ReadLine()!;

                Doctor doctor = new Doctor
                {
                    Name = name,
                    Specialization = specialization,
                    Contact = contact
                };

                int doctorId = _doctorService.AddDoctor(doctor);
                Console.WriteLine($"Doctor added with ID: {doctorId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while adding the doctor: {e.Message}");
            }
        }

        private void GetDoctorById()
        {
            try
            {
                Console.Write("Enter Doctor ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Doctor doctor = _doctorService.GetDoctorById(id);
                if (doctor != null)
                {
                    Console.WriteLine($"Doctor ID: {doctor.Id}, Name: {doctor.Name}, Specialization: {doctor.Specialization}, Contact: {doctor.Contact}");
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the doctor: {e.Message}");
            }
        }

        private void GetDoctorByName()
        {
            try
            {
                Console.Write("Enter Doctor Name: ");
                string name = Console.ReadLine()!;

                Doctor doctor = _doctorService.GetDoctorByName(name);
                if (doctor != null)
                {
                    Console.WriteLine($"Doctor ID: {doctor.Id}, Name: {doctor.Name}, Specialization: {doctor.Specialization}, Contact: {doctor.Contact}");
                }
                else
                {
                    Console.WriteLine("Doctor not found.");
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while retrieving the doctor: {e.Message}");
            }
        }

        private void DeleteDoctor()
        {
            try
            {
                Console.Write("Enter Doctor ID: ");
                int id = int.Parse(Console.ReadLine()!);

                bool success = _doctorService.DeleteDoctor(id);
                if (success)
                {
                    Console.WriteLine("Doctor deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Doctor with ID {id} not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while deleting the doctor: {e.Message}");
            }
        }

        private void ViewAllDoctors()
        {
            try
            {
                var doctors = _doctorService.GetAllDoctors();
                Console.WriteLine("\nAll Doctors:");
                foreach (var doctor in doctors)
                {
                    Console.WriteLine(doctor);
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine($"No doctors found {e.Message}.");
            }
        }

        private void ManagePatients() // New addition
        {
            while (true)
            {
                ClearConsole();
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
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
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
                Console.ReadKey();
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
                Console.ReadKey();
                Console.WriteLine("Press any key to continue");
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

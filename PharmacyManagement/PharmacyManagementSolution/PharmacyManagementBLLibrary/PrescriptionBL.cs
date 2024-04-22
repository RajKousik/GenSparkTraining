using PharmacyManagementBLLibrary.Exceptions;
using PharmacyManagementDALLibrary;
using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public class PrescriptionBL : IPrescriptionService
    {
        private const int LOW_STOCK_THRESHOLD = 4;
        private readonly IRepository<int, Prescription> _prescriptionRepository;
        private readonly IDrugService _drugService;
        private readonly ISaleService _saleService;


        /// <summary>
        /// Provides operations related to prescriptions.
        /// </summary>
        public PrescriptionBL(IDrugService drugService, ISaleService saleService)
        {
            _prescriptionRepository = new PrescriptionRepository();
            _drugService = drugService;
            _saleService = saleService;
        }

        /// <summary>
        /// Process the prescription and add it to the prescription list.
        /// </summary>
        /// <param name="prescription">The prescription object containing details of the prescribed drugs.</param>
        /// <returns>Returns the Prescription ID.</returns>

        public int ProcessPrescription(Prescription prescription)
        {
            // Check the availability of prescribed drugs
            foreach(var drug in prescription.Drugs) 
            { 
                var availableDrug = _drugService.GetDrugByName(drug.Name);
                if (availableDrug == null)
                {
                    throw new DrugNotFoundException();
                }

                if (availableDrug.InStock <= 0)
                {
                    throw new OutOfStockException();
                }

                if (availableDrug.ExpiryDate <= DateTime.Today)
                {
                    throw new ExpiredDrugException(); // Assuming you have a custom exception for expired drugs
                }

                //Console.WriteLine($"Here {availableDrug.InStock} {LOW_STOCK_THRESHOLD}");
                if (availableDrug.InStock < LOW_STOCK_THRESHOLD)
                {
                    Console.WriteLine($"\n**Warning: Low stock for drug '{drug.Name}'. Current stock level: {availableDrug.InStock}**\n");
                }

                
            }

            // Process the prescription
            // ...

            // Save the prescription to the repository
            int prescriptionId = AddPrescription(prescription);


            return prescriptionId;
        }

        /// <summary>
        /// Adds the prescription to the repository.
        /// </summary>
        /// <param name="prescription">The prescription object.</param>
        /// <returns>Returns the Prescription ID.</returns>
        public int AddPrescription(Prescription prescription)
        {
            double totalPrice = 0;
            foreach (var drug in prescription.Drugs)
            {
                totalPrice += drug.Price;
            }

            // Create Sales object
            Sales sale = new Sales
            {
                TransactionId = prescription.Id, // Assuming TransactionId is the same as Prescription Id
                SalesType = "Prescription",
                TotalPrice = totalPrice
            };

            // Add sale
            _saleService.AddSale(sale);
            Prescription result = _prescriptionRepository.Add(prescription);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicatePrescriptionException();
        }

        /// <summary>
        /// Deletes the prescription from the repository.
        /// </summary>
        /// <param name="id">The ID of the prescription to delete.</param>
        /// <returns>Returns true if the prescription is successfully deleted; otherwise, false.</returns>
        public bool DeletePrescription(int id)
        {
            Prescription deletedPrescription = _prescriptionRepository.Delete(id);
            if (deletedPrescription != null)
            {
                return true;
            }
            throw new PrescriptionNotFoundException();
        }
        /// <summary>
        /// Retrieves a prescription by its ID.
        /// </summary>
        /// <param name="id">The ID of the prescription to retrieve.</param>
        /// <returns>Returns the prescription with the specified ID.</returns>
        public Prescription GetPrescriptionById(int id)
        {
            Prescription prescription = _prescriptionRepository.Get(id);
            if (prescription != null)
            {
                return prescription;
            }
            throw new PrescriptionNotFoundException();
        }

        /// <summary>
        /// Retrieves all prescriptions associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>Returns a list of prescriptions associated with the specified patient.</returns>
        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            // Implement logic to retrieve prescriptions by patient ID from the repository
            var prescriptions = _prescriptionRepository.GetAll().Where(p => p.Patient.Id == patientId).ToList();
            if (prescriptions.Count == 0)
            {
                throw new PrescriptionNotFoundException();
            }
            return prescriptions;
        }

        /// <summary>
        /// Retrieves all prescriptions associated with a specific doctor.
        /// </summary>
        /// <param name="doctorId">The ID of the doctor.</param>
        /// <returns>Returns a list of prescriptions associated with the specified doctor.</returns>
        public List<Prescription> GetPrescriptionsByDoctorId(int doctorId)
        {
            // Implement logic to retrieve prescriptions by doctor ID from the repository
            var prescriptions = _prescriptionRepository.GetAll().Where(p => p.Doctor.Id == doctorId).ToList();
            if (prescriptions.Count == 0)
            {
                throw new PrescriptionNotFoundException();
            }
            return prescriptions;
        }

        /// <summary>
        /// Retrieves all prescriptions stored in the repository.
        /// </summary>
        /// <returns>Returns a list of all prescriptions.</returns>
        public List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> prescriptions = _prescriptionRepository.GetAll();
            if (prescriptions != null)
            {
                return prescriptions;
            }
            throw new PrescriptionNotFoundException();
        }
    }
}

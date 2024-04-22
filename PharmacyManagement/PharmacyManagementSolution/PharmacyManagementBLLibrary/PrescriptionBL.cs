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
        private const int LOW_STOCK_THRESHOLD = 2;
        private readonly IRepository<int, Prescription> _prescriptionRepository;
        private readonly IDrugService _drugService;

        public PrescriptionBL()
        {
            _prescriptionRepository = new PrescriptionRepository();
            _drugService = new DrugBL();
        }

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
                else if (availableDrug.InStock < LOW_STOCK_THRESHOLD)
                {
                    Console.WriteLine($"Warning: Low stock for drug '{drug.Name}'. Current stock level: {availableDrug.InStock}");
                }
            }
            

            // Process the prescription
            // ...

            // Save the prescription to the repository
            int prescriptionId = AddPrescription(prescription);

            return prescriptionId;
        }

        public int AddPrescription(Prescription prescription)
        {
            Prescription result = _prescriptionRepository.Add(prescription);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicatePrescriptionException();
        }

        public bool DeletePrescription(int id)
        {
            Prescription deletedPrescription = _prescriptionRepository.Delete(id);
            if (deletedPrescription != null)
            {
                return true;
            }
            throw new PrescriptionNotFoundException();
        }

        public Prescription GetPrescriptionById(int id)
        {
            Prescription prescription = _prescriptionRepository.Get(id);
            if (prescription != null)
            {
                return prescription;
            }
            throw new PrescriptionNotFoundException();
        }

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

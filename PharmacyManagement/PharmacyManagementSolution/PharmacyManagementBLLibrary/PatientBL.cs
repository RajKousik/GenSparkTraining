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
    public class PatientBL : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;

        public PatientBL()
        {
            _patientRepository = new PatientRepository();
        }

        public int AddPatient(Patient patient)
        {
            Patient result = _patientRepository.Add(patient);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicatePatientException();
        }

        public bool DeletePatient(int id)
        {
            var deletedPatient = _patientRepository.Delete(id);
            if (deletedPatient != null)
            {
                return true;
            }
            throw new PatientNotFoundException();
        }

        public Patient GetPatientById(int id)
        {
            var result = _patientRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new PatientNotFoundException();
        }

        public Patient GetPatientByName(string name)
        {
            var patient = _patientRepository.GetAll().Find(p => p.Name == name);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAll();
            if (patients == null)
            {
                throw new PatientNotFoundException();
            }
            return patients;
        }

        public Patient UpdatePatient(Patient patient)
        {
            var result = _patientRepository.Update(patient);
            if (result != null)
            {
                return result;
            }
            throw new PatientNotFoundException();
        }
    }
}

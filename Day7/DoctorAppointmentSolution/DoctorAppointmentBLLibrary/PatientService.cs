using DoctorAppointmentBLLibrary.Exceptions;
using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;

        [ExcludeFromCodeCoverage]
        public PatientService()
        {
            _patientRepository = new PatientRepository();
        }

        [ExcludeFromCodeCoverage]
        public PatientService(IRepository<int, Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public int AddPatient(Patient patient)
        {
            Patient added = _patientRepository.Add(patient);
            return added != null ? added.Id : throw new DuplicatePatientException();
        }

        public Patient DeletePatient(int id)
        {
            Patient deleted = _patientRepository.Delete(id);
            return deleted != null ? deleted : throw new PatientNotFoundException();
        }

        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAll();
            if (patients.Count == 0)
            {
                throw new PatientNotFoundException();
            }
            return patients;
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

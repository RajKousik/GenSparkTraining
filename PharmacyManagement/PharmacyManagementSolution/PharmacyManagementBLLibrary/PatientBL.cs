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

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientBL"/> class.
        /// </summary>
        public PatientBL()
        {
            _patientRepository = new PatientRepository();
        }

        /// <summary>
        /// Adds a new patient to the repository.
        /// </summary>
        /// <param name="patient">The patient object to add.</param>
        /// <returns>Returns the ID of the newly added patient.</returns>
        public int AddPatient(Patient patient)
        {
            Patient result = _patientRepository.Add(patient);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicatePatientException();
        }

        /// <summary>
        /// Deletes a patient from the repository.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>Returns true if the patient is successfully deleted; otherwise, false.</returns>
        public bool DeletePatient(int id)
        {
            var deletedPatient = _patientRepository.Delete(id);
            if (deletedPatient != null)
            {
                return true;
            }
            throw new PatientNotFoundException();
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>Returns the patient with the specified ID.</returns>
        public Patient GetPatientById(int id)
        {
            var result = _patientRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new PatientNotFoundException();
        }

        /// <summary>
        /// Retrieves a patient by their name.
        /// </summary>
        /// <param name="name">The name of the patient to retrieve.</param>
        /// <returns>Returns the patient with the specified name.</returns>
        public Patient GetPatientByName(string name)
        {
            var patient = _patientRepository.GetAll().Find(p => p.Name == name);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            return patient;
        }

        /// <summary>
        /// Retrieves a list of all patients stored in the repository.
        /// </summary>
        /// <returns>Returns a list of all patients.</returns>
        public List<Patient> GetAllPatients()
        {
            var patients = _patientRepository.GetAll();
            if (patients == null)
            {
                throw new PatientNotFoundException();
            }
            return patients;
        }

        /// <summary>
        /// Updates the information of a patient in the repository.
        /// </summary>
        /// <param name="patient">The updated patient object.</param>
        /// <returns>Returns the updated patient.</returns>
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

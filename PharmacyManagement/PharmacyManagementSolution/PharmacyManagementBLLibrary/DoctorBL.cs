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
    public class DoctorBL : IDoctorService
    {
        private readonly IRepository<int, Doctor> _doctorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorBL"/> class.
        /// </summary>
        public DoctorBL()
        {
            _doctorRepository = new DoctorRepository();
        }

        /// <summary>
        /// Adds a new doctor to the repository.
        /// </summary>
        /// <param name="doctor">The doctor object to add.</param>
        /// <returns>Returns the ID of the newly added doctor.</returns>
        public int AddDoctor(Doctor doctor)
        {
            Doctor result = _doctorRepository.Add(doctor);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDoctorException();
        }

        /// <summary>
        /// Deletes a doctor from the repository.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        /// <returns>Returns true if the doctor is successfully deleted; otherwise, false.</returns>
        public bool DeleteDoctor(int id)
        {
            var deletedDoctor = _doctorRepository.Delete(id);
            if (deletedDoctor != null)
            {
                return true;
            }
            throw new DoctorNotFoundException();
        }

        /// <summary>
        /// Retrieves a doctor by their ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to retrieve.</param>
        /// <returns>Returns the doctor with the specified ID.</returns>
        public Doctor GetDoctorById(int id)
        {
            var result = _doctorRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DoctorNotFoundException();
        }

        /// <summary>
        /// Retrieves a doctor by their name.
        /// </summary>
        /// <param name="name">The name of the doctor to retrieve.</param>
        /// <returns>Returns the doctor with the specified name.</returns>
        public Doctor GetDoctorByName(string name)
        {
            var doctor = _doctorRepository.GetAll().Find(d => d.Name == name);
            if (doctor == null)
            {
                throw new DoctorNotFoundException();
            }
            return doctor;
        }

        /// <summary>
        /// Retrieves a list of all doctors stored in the repository.
        /// </summary>
        /// <returns>Returns a list of all doctors.</returns>
        public List<Doctor> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            if (doctors == null)
            {
                throw new DoctorNotFoundException();
            }
            return doctors;
        }

        /// <summary>
        /// Updates information about a doctor in the repository.
        /// </summary>
        /// <param name="doctor">The updated doctor object.</param>
        /// <returns>Returns the updated doctor object.</returns>
        public Doctor UpdateDoctor(Doctor doctor)
        {
            var result = _doctorRepository.Update(doctor);
            if (result != null)
            {
                return result;
            }
            throw new DoctorNotFoundException();
        }
    }
}

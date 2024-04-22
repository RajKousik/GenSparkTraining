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

        public DoctorBL()
        {
            _doctorRepository = new DoctorRepository();
        }

        public int AddDoctor(Doctor doctor)
        {
            Doctor result = _doctorRepository.Add(doctor);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDoctorException();
        }

        public bool DeleteDoctor(int id)
        {
            var deletedDoctor = _doctorRepository.Delete(id);
            if (deletedDoctor != null)
            {
                return true;
            }
            throw new DoctorNotFoundException();
        }

        public Doctor GetDoctorById(int id)
        {
            var result = _doctorRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DoctorNotFoundException();
        }

        public Doctor GetDoctorByName(string name)
        {
            var doctor = _doctorRepository.GetAll().Find(d => d.Name == name);
            if (doctor == null)
            {
                throw new DoctorNotFoundException();
            }
            return doctor;
        }

        public List<Doctor> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            if (doctors == null)
            {
                throw new DoctorNotFoundException();
            }
            return doctors;
        }

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

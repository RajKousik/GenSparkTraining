using DoctorAppointmentBLLibrary.Exceptions;
using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;

namespace DoctorAppointmentBLLibrary
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<int, Doctor> _doctorRepository;

        public DoctorService()
        {
            _doctorRepository = new DoctorRepository();
        }

        public DoctorService(IRepository<int, Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public int AddDoctor(Doctor doctor)
        {
            Doctor result = _doctorRepository.Add(doctor);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDoctorException(doctor.Name);
        }

        public Doctor DeleteDoctor(int id)
        {
            var deletedDoctor = _doctorRepository.Delete(id);
            if (deletedDoctor != null)
            {
                return deletedDoctor;
            }
            throw new DoctorNotFoundException();
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

        public Doctor GetDoctorById(int id)
        {
            var result = _doctorRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DoctorNotFoundException();
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

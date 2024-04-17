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

        public int AddDoctor(Doctor doctor)
        {
            Doctor added = _doctorRepository.Add(doctor);
            return added != null ? added.Id : -1;
        }

        public Doctor DeleteDoctor(int id)
        {
            return _doctorRepository.Delete(id);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor GetDoctorById(int id)
        {
            return _doctorRepository.Get(id);
        }

        public Doctor UpdateDoctor(Doctor doctor)
        {
            return _doctorRepository.Update(doctor);
        }
    }

}

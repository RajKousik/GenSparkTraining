using ClinicManagementAPI.Exceptions;
using ClinicManagementAPI.Interfaces;
using ClinicManagementAPI.Models;

namespace ClinicManagementAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<int, Doctor> _repository;

        public DoctorService(IRepository<int, Doctor> reposiroty)
        {
            _repository = reposiroty;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorBySpeciality(string speciality)
        {
            var doctors = (await _repository.Get()).ToList().FindAll(d => d.Speciality.ToLower().Contains(speciality.ToLower()));
            if (doctors.Count == 0)
                throw new NoSuchDoctorException();
            return doctors;
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            if(doctor == null)
            {
                throw new NoDoctorsFoundException();
            }
            var result = await _repository.Add(doctor);
            return result;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            var doctors = (await _repository.Get()).ToList();
            if (doctors.Count == 0)
                throw new NoDoctorsFoundException();
            return doctors;
        }

        public async Task<Doctor> UpdateDoctorExperience(int id, int experience)
        {
            var doctor = await _repository.Get(id);
            if (doctor == null)
                throw new NoDoctorsFoundException();
            doctor.Experience = experience;
            doctor = await _repository.Update(doctor);
            return doctor;
        }
    }
}

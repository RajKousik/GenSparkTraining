using ClinicManagementAPI.Models;

namespace ClinicManagementAPI.Interfaces
{
    public interface IDoctorService
    {
        public Task<IEnumerable<Doctor>> GetDoctorBySpeciality(string speciality);
        public Task<Doctor> UpdateDoctorExperience(int id, int experience);
        public Task<IEnumerable<Doctor>> GetDoctors();

        public Task<Doctor> AddDoctor(Doctor doctor);
    }
}

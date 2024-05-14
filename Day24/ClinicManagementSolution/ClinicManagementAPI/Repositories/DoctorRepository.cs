using ClinicManagementAPI.Contexts;
using ClinicManagementAPI.Exceptions;
using ClinicManagementAPI.Interfaces;
using ClinicManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementAPI.Repositories
{
    public class DoctorRepository : IRepository<int, Doctor>
    {
        private readonly ClinicManagementDbContext _context;
        public DoctorRepository(ClinicManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Doctor> Add(Doctor item)
        {
            if(item == null)
            {
                throw new NoSuchDoctorException();
            }
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Doctor> Delete(int key)
        {
            var doctor = await Get(key);
            if (doctor != null)
            {
                _context.Remove(doctor);
                await _context.SaveChangesAsync(true);
                return doctor;
            }
            throw new NoSuchDoctorException();
        }

        public Task<Doctor> Get(int key)
        {
            var doctor = _context.Doctors.FirstOrDefaultAsync(e => e.Id == key);
            if(doctor == null)
            {
                throw new NoSuchDoctorException();
            }
            return doctor;
        }

        public async Task<IEnumerable<Doctor>> Get()
        {
            var doctors = await _context.Doctors.ToListAsync();
            if(doctors.Count == 0)
            {
                throw new NoDoctorsFoundException();
            }
            return doctors;

        }

        public async Task<Doctor> Update(Doctor item)
        {
            var doctor = await Get(item.Id);
            if (doctor != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync(true);
                return doctor;
            }
            throw new NoSuchDoctorException();
        }
    }
}

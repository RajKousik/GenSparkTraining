using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public interface IDoctorService
    {
        int AddDoctor(Doctor doctor);
        Doctor GetDoctorById(int id);
        Doctor GetDoctorByName(string name);
        List<Doctor> GetAllDoctors();
        Doctor UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
    }
}

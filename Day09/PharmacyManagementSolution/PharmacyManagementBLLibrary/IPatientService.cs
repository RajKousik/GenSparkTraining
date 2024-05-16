using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public interface IPatientService
    {
        int AddPatient(Patient patient);
        Patient GetPatientById(int id);
        Patient GetPatientByName(string name);
        List<Patient> GetAllPatients();
        Patient UpdatePatient(Patient patient);
        bool DeletePatient(int id); 
    }
}

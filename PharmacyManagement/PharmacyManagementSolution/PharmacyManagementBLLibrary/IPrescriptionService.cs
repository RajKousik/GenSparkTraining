using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public interface IPrescriptionService
    {
        int AddPrescription(Prescription prescription);
        bool DeletePrescription(int id);
        Prescription GetPrescriptionById(int id);
        List<Prescription> GetPrescriptionsByPatientId(int patientId);
        List<Prescription> GetPrescriptionsByDoctorId(int doctorId);
        List<Prescription> GetAllPrescriptions();

        int ProcessPrescription(Prescription prescription);
    }
}

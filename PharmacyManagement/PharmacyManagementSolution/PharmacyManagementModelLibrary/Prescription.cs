using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmacyManagementModelLibrary
{
    public class Prescription
    {
        public int Id{ get; set; }
        public Patient Patient{ get; set; }

        public Doctor Doctor{ get; set; }

        public Drugs Drug{ get; set; }

        public int Dosage{ get; set; }

        public Prescription(Patient patient, Doctor doctor, Drugs drug, int dosage)
        {
            Patient = patient;
            Doctor = doctor;
            Drug = drug;
            Dosage = dosage;
        }

        public override string ToString()
        {
            return "\nPrescription Id" + Id
                + "\nPatient Name : " + Patient.Name
                + "\nDoctor Name : " + Doctor.Name
                + "\nDrug : " + Drug.Name
                + "\nDosage : " + Dosage + "\n";
        }
    }
}

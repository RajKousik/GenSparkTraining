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
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public List<Drugs> Drugs { get; set; } // Change type to List<Drugs>
        public int Dosage { get; set; }

        public Prescription(Patient patient, Doctor doctor, List<Drugs> drugs, int dosage)
        {
            Patient = patient;
            Doctor = doctor;
            Drugs = drugs;
            Dosage = dosage;
        }

        public override string ToString()
        {
            return "\nPrescription Id" + Id
                + "\nPatient Name : " + Patient.Name
                + "\nDoctor Name : " + Doctor.Name
                + "\nDrug : " + Drugs
                + "\nDosage : " + Dosage + "\n";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorAppointmentModelLibrary
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateTime { get; set; }

        public Appointment(int doctorId, int patientId, DateTime dateTime)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            DateTime = dateTime;
        }


        public override string ToString()
        {
            return "Appointment ID : " + Id
                + "\nDoctor : " + DoctorId
                + "\nPatient : " + PatientId
                + "\nAppointment Date  : " + DateTime + "\n";

        }
        public override bool Equals(object? obj)
        {
            Appointment a1, a2;
            a1 = this;
            a2 = obj as Appointment;//Casting in a more symanctic way
            return a1.Id.Equals(a2.Id);
        }
    }
}

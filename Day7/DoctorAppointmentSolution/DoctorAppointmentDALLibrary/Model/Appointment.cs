using System;
using System.Collections.Generic;

namespace DoctorAppointmentDALLibrary.Model
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }

        public Appointment(int? doctorId, int? patientId, DateTime dateTime)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            DateTime = dateTime;
        }

        public Appointment()
        {
            DoctorId = null;
            PatientId = null;
            DateTime = null;

        }

        public override string ToString()
        {
            return "Appointment ID : " + Id
                + "\nDoctor : " + DoctorId
                + "\nPatient : " + PatientId
                + "\nAppointment Date  : " + DateTime + "\n";

        }
        //public override bool Equals(object? obj)
        //{
        //    Appointment a1, a2;
        //    a1 = this;
        //    a2 = obj as Appointment;//Casting in a more symanctic way
        //    return a1.Id.Equals(a2.Id);
        //}
    }
}

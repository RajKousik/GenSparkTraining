using System;
using System.Collections.Generic;

namespace DoctorAppointmentDALLibrary.Model
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            Name = "";
            Speciality = "";
            Contact = "";
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Speciality { get; set; }
        public string? Contact { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public Doctor(string? name, string? specialty, string? contact)
        {
            Name = name;
            Speciality = specialty;
            Contact = contact;
        }

        public override string ToString()
        {
            return "Doctor ID : " + Id
                + "\nDoctor Name : " + Name
                + "\nSpeciality : " + Speciality
                + "\nContact : " + Contact + "\n";

        }

    }
}

using System;
using System.Collections.Generic;

namespace DoctorAppointmentDALLibrary.Model
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Contact { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public Patient(string? name, DateTime dateOfBirth, string? contact, string? address)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Contact = contact;
            Address = address;
        }

        public override string ToString()
        {
            return "Patient ID : " + Id
                + "\nPatient Name : " + Name
                + "\nAge : " + Age
                + "\nDOB: " + DateOfBirth
                + "\nContact: " + Contact
                + "\nAddress: " + Address + "\n";

        }
        public override bool Equals(object? obj)
        {
            Patient p1, p2;
            p1 = this;
            p2 = obj as Patient;//Casting in a more symanctic way
            return p1.Id.Equals(p2.Id);
        }
    }
}

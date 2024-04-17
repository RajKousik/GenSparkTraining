using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorAppointmentModelLibrary
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Specialty { get; set; } = string.Empty;
        public string? Contact { get; set; } = string.Empty;

        public Doctor(string? name, string? specialty, string? contact)
        {
            Name = name;
            Specialty = specialty;
            Contact = contact;
        }

        public override string ToString()
        {
            return "Doctor ID : " + Id
                + "\nDoctor Name : " + Name
                + "\nSpeciality : " + Specialty
                + "\nContact : " + Contact + "\n";
                
        }
        public override bool Equals(object? obj)
        {
            Doctor d1, d2;
            d1 = this;
            d2 = obj as Doctor;//Casting in a more symanctic way
            return d1.Id.Equals(d2.Id);
        }

    }
}

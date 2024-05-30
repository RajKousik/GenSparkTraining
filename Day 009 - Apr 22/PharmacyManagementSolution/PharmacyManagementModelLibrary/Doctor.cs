using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementModelLibrary
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;

        public Doctor()
        {
            Name = "";
            Specialization = "";
            Contact = "";
        }

        public Doctor(string name, string specialization, string contact)
        {
            Name = name;
            Specialization = specialization;
            Contact = contact;
        }

        public override string ToString()
        {
            return "\nDoctor Id : " + Id
                + "\nDoctor Name : " + Name
                + "\nSpecialization : " + Specialization
                + "\nContact : " + Contact + "\n";
        }

        public override bool Equals(object? obj)
        {
            Doctor d1, d2;
            d1 = this;
            d2 = obj as Doctor;
            return d1.Id.Equals(d2.Id);
        }
    }
}

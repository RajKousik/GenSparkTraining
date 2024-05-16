using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementModelLibrary
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Contact { get; set; } = string.Empty;

        public Patient()
        {
            Name = "";
            DateOfBirth = DateTime.MinValue;
            Contact = "";
        }

        public Patient(string name, DateTime dateOfBirth, string contact)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Contact = contact;
        }

        public override string ToString()
        {
            return "\nPatient Id : " + Id
                + "\nPatient Name : " + Name
                + "\nDate of Birth : " + DateOfBirth.ToString("yyyy-MM-dd")
                + "\nContact : " + Contact + "\n";
        }

        public override bool Equals(object? obj)
        {
            Patient p1, p2;
            p1 = this;
            p2 = obj as Patient;
            return p1.Id.Equals(p2.Id);
        }
    }
}

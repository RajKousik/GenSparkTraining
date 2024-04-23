namespace DoctorAppointmentModelLibrary
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        int age;
        DateTime dob;
        public int Age
        {
            get
            {
                return age;
            }
        }
        public DateTime DateOfBirth 
        {
            get => dob;
            set {
                dob = value;
                age = ((DateTime.Today - dob).Days) / 365;
            }
        }
        public string? Contact { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        public string? PatientIllness { get; set; } = string.Empty;

        public Patient()
        {
            Name = "";
        }

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
                + "\nAge : " + age
                + "\nDOB: " + dob
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

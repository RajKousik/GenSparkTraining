using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    public class DuplicatePatientException : Exception
    {
        string msg;
        public DuplicatePatientException()
        {
            msg = "Patient with these details already exists.";
        }

        public DuplicatePatientException(string name)
        {
            msg = $"Doctor with these details {name} already exists.";
        }
        public override string Message => msg;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    internal class PatientNotFoundException : Exception
    {
        string msg;
        public PatientNotFoundException()
        {
            msg = "Doctor with these details does not exist";
        }

        public PatientNotFoundException(string name)
        {
            msg = $"Doctor with these details {name} does not exist";
        }
        public override string Message => msg;
    }
}

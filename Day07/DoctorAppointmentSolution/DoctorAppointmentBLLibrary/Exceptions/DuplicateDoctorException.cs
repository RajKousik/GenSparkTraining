using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    internal class DuplicateDoctorException : Exception
    {
        string msg;
        public DuplicateDoctorException()
        {
            msg = "Doctor already exists";
        }

        public DuplicateDoctorException(string name)
        {
            msg = $"Doctor with these details {name} does not exist";
        }
        public override string Message => msg;
    }
}

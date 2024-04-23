using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    public class DoctorNotFoundException : Exception
    {
        string msg;
        public DoctorNotFoundException()
        {
            msg = "Doctor with these details does not exist";
        }

        public DoctorNotFoundException(string name)
        {
            msg = $"Doctor with these details {name} does not exist";
        }
        public override string Message => msg;
    }
}

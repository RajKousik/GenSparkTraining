using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    public class DuplicateAppointmentException : Exception
    {
        string msg;
        public DuplicateAppointmentException()
        {
            msg = "Doctor with these details already exist";
        }

        [ExcludeFromCodeCoverage]
        public override string Message => msg;
    }
}

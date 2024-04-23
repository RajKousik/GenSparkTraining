using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [ExcludeFromCodeCoverage]
        public override string Message => msg;
    }
}

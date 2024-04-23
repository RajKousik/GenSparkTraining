using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        string msg;
        public PatientNotFoundException()
        {
            msg = "Doctor with these details does not exist";
        }

        [ExcludeFromCodeCoverage]
        public override string Message => msg;
    }
}

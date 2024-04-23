using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary.Exceptions
{
    public class AppointmentNotFoundException : Exception
    {
        string msg;
        public AppointmentNotFoundException()
        {
            msg = "Appointment with these details does not exist";
        }

        [ExcludeFromCodeCoverage]
        public override string Message => msg;
    }
}

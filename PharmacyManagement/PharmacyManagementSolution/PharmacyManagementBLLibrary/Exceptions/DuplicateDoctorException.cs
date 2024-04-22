using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    internal class DuplicateDoctorException : Exception
    {
        string msg;
        public DuplicateDoctorException()
        {
            msg = "Doctor already exists";
        }
        public override string Message => msg;

    }
}

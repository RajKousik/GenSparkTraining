using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    internal class DuplicatePatientException : Exception
    {
        string msg;
        public DuplicatePatientException()
        {
            msg = "Patient already exists";
        }
        public override string Message => msg;

    }
}

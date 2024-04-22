using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    internal class PatientNotFoundException : Exception
    {
        string msg;
        public PatientNotFoundException()
        {
            msg = "Patient with these details does not exist";
        }
        public override string Message => msg;

    }
}

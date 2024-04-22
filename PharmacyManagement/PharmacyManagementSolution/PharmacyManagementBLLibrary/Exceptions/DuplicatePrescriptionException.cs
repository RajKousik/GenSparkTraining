using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class DuplicatePrescriptionException : Exception
    {
        string msg;
        public DuplicatePrescriptionException()
        {
            msg = "Prescription already exists";
        }
        public override string Message => msg;

    }
}

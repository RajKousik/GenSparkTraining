using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class PrescriptionNotFoundException : Exception
    {
        string msg;
        public PrescriptionNotFoundException()
        {
            msg = "Prescription with these details does not exist";
        }
        public override string Message => msg;

    }
}

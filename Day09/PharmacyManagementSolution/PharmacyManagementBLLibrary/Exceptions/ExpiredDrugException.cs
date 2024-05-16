using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    internal class ExpiredDrugException : Exception
    {
        string msg;
        public ExpiredDrugException()
        {
            msg = "The Drug has been expired";
        }
        public override string Message => msg;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class DuplicateSaleException : Exception
    {
        string msg;
        public DuplicateSaleException()
        {
            msg = "Sale already exists";
        }
        public override string Message => msg;

    }
}

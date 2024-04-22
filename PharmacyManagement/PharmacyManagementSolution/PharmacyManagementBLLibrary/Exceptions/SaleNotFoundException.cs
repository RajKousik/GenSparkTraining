using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class SaleNotFoundException : Exception
    {
        string msg;
        public SaleNotFoundException()
        {
            msg = "Sale Transaction with these details does not exist";
        }
        public override string Message => msg;

    }
}

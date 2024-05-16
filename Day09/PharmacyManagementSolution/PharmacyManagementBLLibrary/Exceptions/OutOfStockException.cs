using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class OutOfStockException : Exception
    {
        string msg;
        public OutOfStockException()
        {
            msg = "The Prescribed Drug is out of Stock.";
        }
        public override string Message => msg;

    }
}

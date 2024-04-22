using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    public class DrugNotFoundException : Exception
    {
        string msg;
        public DrugNotFoundException()
        {
            msg = "Drug with these details does not exist";
        }
        public override string Message => msg;

    }
}

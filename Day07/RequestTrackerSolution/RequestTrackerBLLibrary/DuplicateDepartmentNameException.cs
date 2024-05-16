using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class DuplicateDepartmentNameException : Exception
    {
        string msg;
        public DuplicateDepartmentNameException() {
            msg = "Department Name already Exists";
        }

        public override string Message => msg;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class DuplicateEmployeeException : Exception
    {
        string msg;
        public DuplicateEmployeeException()
        {
            msg = "Employee already Exists";
        }

        public override string Message => msg;
    }
}

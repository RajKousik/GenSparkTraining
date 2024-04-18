using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerBLLibrary
{
    public class DepartmentNotFoundException : Exception
    {
        string msg;
        public DepartmentNotFoundException()
        {
            msg = "Department with these details does not exist";
        }
        public override string Message => msg;

    }
}

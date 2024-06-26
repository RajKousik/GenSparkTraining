﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary.Exceptions
{
    internal class DuplicateDrugException : Exception
    {
        string msg;
        public DuplicateDrugException()
        {
            msg = "Drug already exists";
        }
        public override string Message => msg;

    }
}

using RequestTrackerModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerApplication
{
    internal class Company
    {

        public void EmployeeClientVisit(IClientInteraction clientInteraction, IInternalCompanyWorking internalCompany)
        {
            clientInteraction.GetOrder();
            clientInteraction.GetPayment();
            internalCompany.RaiseRequest();
            internalCompany.CloseRequest();
        }

    }
}

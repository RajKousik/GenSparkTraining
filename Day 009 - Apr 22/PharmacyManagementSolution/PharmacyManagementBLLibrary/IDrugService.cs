using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public interface IDrugService
    {
        int AddDrug(Drugs drug);
        Drugs GetDrugById(int id);
        Drugs GetDrugByName(string name);
        Drugs UpdateDrug(Drugs drug);
        List<Drugs> GetDrugList();
        bool DeleteDrug(int id);
        bool RemoveExpiredDrugs();
        bool RemoveOutOfStockDrugs();

        void ReduceStockQuantity(string drugName);
    }
}

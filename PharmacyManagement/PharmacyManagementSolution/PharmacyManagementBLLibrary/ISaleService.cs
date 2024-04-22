using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementBLLibrary
{
    public interface ISaleService
    {
        int AddSale(Sales sale);
        Sales GetSaleById(int id);
        List<Sales> GetAllSales();
    }
}

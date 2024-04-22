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

        double CalculateLoyaltyPoints(double totalPrice);
        double ApplyDiscount(double totalPrice, double loyaltyPoints); 
    }
}

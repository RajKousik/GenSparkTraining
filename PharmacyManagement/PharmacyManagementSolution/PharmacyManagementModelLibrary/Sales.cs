using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementModelLibrary
{
    public class Sales
    {
        public int SaleId{ get; set; }

        public int TransactionId { get; set; }

        public string SalesType { get; set; } = string.Empty;

        public double TotalPrice { get; set;}

        public double LoyaltyPointsEarned { get; set; } 

        public Sales()
        {
            LoyaltyPointsEarned = 0;
        }
    }
}

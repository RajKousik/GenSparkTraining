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

        public string SalesType { get; set; } = string.Empty;

        public float TotalPrice { get; set;}
    }
}

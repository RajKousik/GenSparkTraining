using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }//Navigation property
        public double TotalPrice { get; set; }
        public List<CartItem> CartItems { get; set; }//Navigation property
    }
}

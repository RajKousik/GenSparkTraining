using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }//Navigation property
        
        public int ProductId { get; set; }
        public Product Product { get; set; }//Navigation property
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public DateTime PriceExpiryDate { get; set; }
    }
}

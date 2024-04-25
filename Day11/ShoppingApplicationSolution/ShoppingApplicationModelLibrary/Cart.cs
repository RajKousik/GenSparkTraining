using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingApplicationModelLibrary
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }//Navigation property
        public double TotalPrice { get; set; }
        public List<CartItem> CartItems { get; set; }//Navigation property

        public override string ToString()
        {
            string cartItemsNames = string.Join(", ", CartItems.Select(item => item.Product.Name));

            return $"Cart Id: {Id}, Customer Name: {Customer.Name}, Cart Total Price: {TotalPrice}, Cart Items: {cartItemsNames}";
        }

    }
}

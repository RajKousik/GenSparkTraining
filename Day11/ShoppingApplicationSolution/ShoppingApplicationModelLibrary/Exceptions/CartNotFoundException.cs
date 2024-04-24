using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary.Exceptions
{
    public class CartNotFoundException  : Exception
    {
        string message;
        public CartNotFoundException()
        {
            message = "Cart with the given Id is not present";
        }
        public override string Message => message;
    }
}

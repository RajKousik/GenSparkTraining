using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary.Exceptions
{
    public class NoCartItemWithGivenIdException : Exception
    {
        string message;
        public NoCartItemWithGivenIdException()
        {
            message = "Cart Item with the given Id is not present";
        }
        public override string Message => message;
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [ExcludeFromCodeCoverage]
        public override string Message => message;
    }
}

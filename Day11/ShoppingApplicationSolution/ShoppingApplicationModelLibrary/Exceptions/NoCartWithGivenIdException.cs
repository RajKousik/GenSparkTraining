using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary.Exceptions
{
    public class NoCartWithGivenIdException : Exception
    {
        string message;
        public NoCartWithGivenIdException()
        {
            message = "Cart with the given Id is not present";
        }
        [ExcludeFromCodeCoverage]
        public override string Message => message;
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplicationModelLibrary.Exceptions
{
    public class NoProductWithGivenIdException : Exception
    {
        string message;
        public NoProductWithGivenIdException()
        {
            message = "Product with the given Id is not present";
        }
        [ExcludeFromCodeCoverage]
        public override string Message => message;
    }
}

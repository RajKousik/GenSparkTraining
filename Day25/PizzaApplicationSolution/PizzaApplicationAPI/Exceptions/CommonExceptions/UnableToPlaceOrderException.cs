using System.Globalization;
using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.CommonExceptions
{
    [Serializable]
    public class UnableToPlaceOrderException : Exception
    {
        private string message;
        public UnableToPlaceOrderException()
        {
            message = "Something went wrong! Not able to place the order ";
        }

        public UnableToPlaceOrderException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
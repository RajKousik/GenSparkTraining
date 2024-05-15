using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class UnableToPlaceOrderException : Exception
    {
        public UnableToPlaceOrderException()
        {
        }

        public UnableToPlaceOrderException(string? message) : base(message)
        {
        }

        public UnableToPlaceOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToPlaceOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
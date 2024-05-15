using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class NoSuchPizzaException : Exception
    {
        public NoSuchPizzaException()
        {
        }

        public NoSuchPizzaException(string? message) : base(message)
        {
        }

        public NoSuchPizzaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchPizzaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
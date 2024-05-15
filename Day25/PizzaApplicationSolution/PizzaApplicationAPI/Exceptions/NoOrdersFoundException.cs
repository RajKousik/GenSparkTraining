using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class NoOrdersFoundException : Exception
    {
        public NoOrdersFoundException()
        {
        }

        public NoOrdersFoundException(string? message) : base(message)
        {
        }

        public NoOrdersFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoOrdersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class NoSuchOrderException : Exception
    {
        public NoSuchOrderException()
        {
        }

        public NoSuchOrderException(string? message) : base(message)
        {
        }

        public NoSuchOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
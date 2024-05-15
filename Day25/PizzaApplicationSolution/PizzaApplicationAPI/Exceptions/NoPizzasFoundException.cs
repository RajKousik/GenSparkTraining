using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class NoPizzasFoundException : Exception
    {
        public NoPizzasFoundException()
        {
        }

        public NoPizzasFoundException(string? message) : base(message)
        {
        }

        public NoPizzasFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoPizzasFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    internal class NoUsersFoundException : Exception
    {
        public NoUsersFoundException()
        {
        }

        public NoUsersFoundException(string? message) : base(message)
        {
        }

        public NoUsersFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoUsersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
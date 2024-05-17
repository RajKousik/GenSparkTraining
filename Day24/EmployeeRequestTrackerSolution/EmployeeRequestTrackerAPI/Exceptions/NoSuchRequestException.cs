using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class NoSuchRequestException : Exception
    {
        private string msg;
        public NoSuchRequestException()
        {
            msg = "No Such Request Found in the Database";
        }

        public override string Message => msg;

        public NoSuchRequestException(string? message) : base(message)
        {
        }

        public NoSuchRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
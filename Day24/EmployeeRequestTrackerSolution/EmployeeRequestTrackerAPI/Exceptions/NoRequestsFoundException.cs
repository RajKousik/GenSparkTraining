using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class NoRequestsFoundException : Exception
    {
        private string msg;
        public NoRequestsFoundException()
        {
            msg = "No Requests Found in the Database";
        }

        public override string Message => msg;

        public NoRequestsFoundException(string? message) : base(message)
        {
        }

        public NoRequestsFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoRequestsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
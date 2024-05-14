using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class NoSuchEmployeeException : Exception
    {
        private string msg;
        public NoSuchEmployeeException()
        {
            msg = "No Such Employee Found in the Database";
        }

        public override string Message => msg;

        public NoSuchEmployeeException(string? message) : base(message)
        {
        }

        public NoSuchEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
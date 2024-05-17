using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class UnableToAddRequestException : Exception
    {
        private string msg;
        public UnableToAddRequestException()
        {
            msg = "Unable to Add Request. Please Try again later";
        }

        public override string Message => msg;

        public UnableToAddRequestException(string? message) : base(message)
        {
        }

        public UnableToAddRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
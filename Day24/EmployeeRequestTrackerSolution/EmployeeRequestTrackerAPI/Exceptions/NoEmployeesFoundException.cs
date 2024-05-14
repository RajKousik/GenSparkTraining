using System.Runtime.Serialization;

namespace EmployeeRequestTrackerAPI.Exceptions
{
    [Serializable]
    internal class NoEmployeesFoundException : Exception
    {
        private string msg;
        public NoEmployeesFoundException()
        {
            msg = "No Employees found in the database";
        }

        public override string Message => msg;

        public NoEmployeesFoundException(string? message) : base(message)
        {
        }

        public NoEmployeesFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoEmployeesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
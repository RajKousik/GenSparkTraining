using System.Runtime.Serialization;

namespace ClinicManagementAPI.Exceptions
{
    [Serializable]
    internal class NoDoctorsFoundException : Exception
    {
        private readonly string message;
        public NoDoctorsFoundException()
        {
            message = "No Doctors Found in the Database";
        }

        public override string Message => message;
    }
}
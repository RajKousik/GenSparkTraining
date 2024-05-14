using System.Runtime.Serialization;

namespace ClinicManagementAPI.Exceptions
{
    [Serializable]
    public class NoSuchDoctorException : Exception
    {
        private readonly string message;
        public NoSuchDoctorException()
        {
            message = "No Doctor with given Details Found in the Database";
        }

        public override string Message => message;


    }
}
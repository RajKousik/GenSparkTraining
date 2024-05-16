using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.CommonExceptions
{
    [Serializable]
    public class UnauthorizedUserException : Exception
    {
        private string message;
        public UnauthorizedUserException()
        {
            message = "UnAuthorized User Exception";
        }
        public UnauthorizedUserException(string message)
        {
            this.message = message;
        }


        public override string Message => message;
    }
}
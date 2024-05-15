using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    public class UnauthorizedUserException : Exception
    {
        private string message;
        public UnauthorizedUserException()
        {
            message = "UnAuthorized User Exception";
        }
        public UnauthorizedUserException(String message)
        {
            this.message = message;
        }


        public override string Message => message;
    }
}
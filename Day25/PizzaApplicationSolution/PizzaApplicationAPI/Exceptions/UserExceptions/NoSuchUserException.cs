using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.UserExceptions
{
    [Serializable]
    public class NoSuchUserException : Exception
    {
        private string message;
        public NoSuchUserException()
        {
            message = "User with the given detail Not Found in the Database";
        }

        public override string Message => message;
    }
}
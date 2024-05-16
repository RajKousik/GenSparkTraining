using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.UserExceptions
{
    [Serializable]
    public class DuplicateUserException : Exception
    {
        private string message;
        public DuplicateUserException()
        {
            message = "User with the same mail id already Exists";
        }

        public override string Message => message;
    }
}
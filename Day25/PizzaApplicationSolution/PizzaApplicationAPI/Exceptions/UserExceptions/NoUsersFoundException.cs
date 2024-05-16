using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.UserExceptions
{
    [Serializable]
    public class NoUsersFoundException : Exception
    {
        private string message;
        public NoUsersFoundException()
        {
            message = "No Users Found in the Database";
        }

        public override string Message => message;
    }
}
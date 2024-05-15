using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    public class UnableToRegisterException : Exception
    {
        private string message;
        public UnableToRegisterException()
        {
            message = "Unable to register!";
        }

        public UnableToRegisterException(string message)
        {
           this.message = message;
        }

        public override string Message => message;
    }
}
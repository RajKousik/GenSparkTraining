using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.OrderExceptions
{
    [Serializable]
    public class NoSuchOrderException : Exception
    {
        private string message;
        public NoSuchOrderException()
        {
            message = "Order given the detail not Found in the Database";
        }

        public NoSuchOrderException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
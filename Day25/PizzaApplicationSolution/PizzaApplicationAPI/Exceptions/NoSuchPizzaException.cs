using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    public class NoSuchPizzaException : Exception
    {
        private string message;
        public NoSuchPizzaException()
        {
            message = "Pizza with given detail not found in the Database";
        }

        public override string Message => message;
    }
}
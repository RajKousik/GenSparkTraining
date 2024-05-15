using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions
{
    [Serializable]
    public class NoPizzasFoundException : Exception
    {
        private string message;
        public NoPizzasFoundException()
        {
            message = "No Pizzas Found in the Database";
        }

        public override string Message => message;
    }
}
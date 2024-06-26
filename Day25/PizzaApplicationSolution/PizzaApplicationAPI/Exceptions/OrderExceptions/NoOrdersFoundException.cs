﻿using System.Runtime.Serialization;

namespace PizzaApplicationAPI.Exceptions.OrderExceptions
{
    [Serializable]
    public class NoOrdersFoundException : Exception
    {
        private string message;
        public NoOrdersFoundException()
        {
            message = "No Order Found in the Database";
        }

        public override string Message => message;


    }
}
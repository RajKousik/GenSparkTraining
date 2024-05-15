using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models.DTOs
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
    }
}

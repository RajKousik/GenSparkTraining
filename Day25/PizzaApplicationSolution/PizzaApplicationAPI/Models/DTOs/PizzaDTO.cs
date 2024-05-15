using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models.DTOs
{
    public class PizzaDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}

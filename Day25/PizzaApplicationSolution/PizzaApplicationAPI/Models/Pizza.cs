using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Price { get; set;}
    }
}

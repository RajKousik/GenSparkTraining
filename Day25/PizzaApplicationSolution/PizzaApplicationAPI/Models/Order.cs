using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaApplicationAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PizzaId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("PizzaId")]
        public Pizza? Pizza { get; set; }
    }
}

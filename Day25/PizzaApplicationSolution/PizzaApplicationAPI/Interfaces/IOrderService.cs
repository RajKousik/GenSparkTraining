using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> AddOrder(Order orderDTO);
        public Task<IEnumerable<Order>> GetOrders();

    }
}

using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderDTO> AddOrder(OrderDTO orderDTO);
        public Task<IEnumerable<OrderDTO>> GetOrders();

    }
}

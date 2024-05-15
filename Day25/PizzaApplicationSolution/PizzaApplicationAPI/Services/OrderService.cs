using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models.DTOs;
using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _repository;

        public OrderService(IRepository<int, Order> repository)
        {
            _repository = repository;
        }
        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            try
            {
                var order = MapOrderDTOToOrder(orderDTO);
                order = await _repository.Add(order);
                if (order == null)
                {
                    throw new UnableToPlaceOrderException("Not able to place order at this moment");
                }
                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new UnableToPlaceOrderException($"Not able to place order at this moment : {ex.Message}");
            }
        }

        private Order MapOrderDTOToOrder(OrderDTO orderDTO)
        {
            Order order = new Order()
            {
                UserId = orderDTO.UserId,
                PizzaId = orderDTO.PizzaId,
                Quantity = orderDTO.Quantity,
            };

            return order;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            try
            {
                var orders = await _repository.GetAll();
                if (orders == null)
                {
                    throw new NoSuchOrderException();
                }
                IList<OrderDTO> orderDTOs = new List<OrderDTO>();
                foreach (Order order in orders)
                {
                    orderDTOs.Add(MapOrderToOrderDTO(order));
                }

                return orderDTOs;
            }
            catch (Exception ex)
            {
                throw new NoSuchOrderException($"No order with the mentioned details found: {ex.Message}");
            }
        }

        private OrderDTO MapOrderToOrderDTO(Order order)
        {
            OrderDTO orderDTO = new OrderDTO()
            {
                UserId = order.UserId,
                PizzaId = order.PizzaId,
                Quantity = order.Quantity
            };
            return orderDTO;
        }
    }

}

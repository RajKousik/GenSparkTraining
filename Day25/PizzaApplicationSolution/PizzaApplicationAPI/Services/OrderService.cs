using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models.DTOs;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Exceptions.CommonExceptions;
using PizzaApplicationAPI.Exceptions.OrderExceptions;

namespace PizzaApplicationAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _repository;

        public OrderService(IRepository<int, Order> repository)
        {
            _repository = repository;
        }
        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                order = await _repository.Add(order);
                if (order == null)
                {
                    throw new UnableToPlaceOrderException("Not able to place order at this moment");
                }
                return order;
            }
            catch (Exception ex)
            {
                throw new UnableToPlaceOrderException($"Not able to place order at this moment : {ex.Message}");
            }
        }

        //private Order MapOrderDTOToOrder(OrderDTO order)
        //{
        //    Order order = new Order()
        //    {
        //        UserId = orderDTO.UserId,
        //        PizzaId = orderDTO.PizzaId,
        //        Quantity = orderDTO.Quantity,
        //    };

        //    return order;
        //}

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = await _repository.GetAll();
                if (orders == null)
                {
                    throw new NoSuchOrderException();
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw new NoSuchOrderException($"No order with the mentioned details found: {ex.Message}");
            }
        }

        //private OrderDTO MapOrderToOrderDTO(Order order)
        //{
        //    OrderDTO orderDTO = new OrderDTO()
        //    {
        //        UserId = order.UserId,
        //        PizzaId = order.PizzaId,
        //        Quantity = order.Quantity
        //    };
        //    return orderDTO;
        //}
    }

}

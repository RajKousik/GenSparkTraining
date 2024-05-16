using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        #region POST
        [HttpPost("Place Order")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDTO>> Add(OrderDTO orderDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                var result = await _orderService.AddOrder(order);
                return Ok(_mapper.Map<OrderDTO>(result));
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Unable to add order"
                });
            }
        }
        #endregion

        #region GET
        [HttpGet("Get Orders")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<OrderDTO>>> Get()
        {
            try
            {
                var orders = await _orderService.GetOrders();

                var ordersDTO = _mapper.Map<IList<OrderDTO>>(orders);
                return Ok(ordersDTO);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Order not found"
                });
            }
        }
        #endregion
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;
using System.Numerics;

namespace PizzaApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<PizzaDTO>>> Get()
        {
            try
            {
                var pizzas = await _pizzaService.GetAll();
                return Ok(pizzas.ToList());
            }
            catch (NoPizzasFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IList<PizzaDTO>>> AddPizza([FromBody] PizzaDTO pizza)
        {
            try
            {
                var result = await _pizzaService.AddPizza(pizza);
                return Ok(result);
            }
            catch (NoPizzasFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PizzaDTO>> UpdateStock(int id, [FromBody] int stock)
        {
            try
            {
                var pizza = await _pizzaService.UpdateStock(id, stock);
                return Ok(pizza);
            }
            catch (NoSuchPizzaException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPizzaByName")]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<PizzaDTO>>> GetBySpeciality(string name)
        {
            try
            {
                var pizzas = await _pizzaService.GetPizzaByName(name);
                return Ok(pizzas);
            }
            catch (NoPizzasFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

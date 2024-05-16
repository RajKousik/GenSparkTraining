using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApplicationAPI.Exceptions.PizzaExceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;
using System.Numerics;

namespace PizzaApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        private readonly IMapper _mapper;

        public PizzaController(IPizzaService pizzaService, IMapper mapper)
        {
            _pizzaService = pizzaService;
            _mapper = mapper;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<PizzaDTO>>> Get()
        {
            try
            {
                Pizza dummyPizza = new Pizza();
                var pizzas = await _pizzaService.GetAll();
                var pizzaDto = _mapper.Map<IList<PizzaDTO>>(pizzas);
                return Ok(pizzas.ToList());
            }
            catch (NoPizzasFoundException ex)
            {
                return NotFound($"Unable to Fetch Pizza : {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PizzaDTO>> AddPizza([FromBody] PizzaDTO pizzaDTO)
        {
            try
            {
                var pizza = _mapper.Map<Pizza>(pizzaDTO);
                var result = await _pizzaService.AddPizza(pizza);
                var resultDTO = _mapper.Map<PizzaDTO>(result);
                return Ok(resultDTO);
            }
            catch (NoPizzasFoundException ex)
            {
                return BadRequest($"Unable to Add Pizza : {ex.Message}");
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PizzaDTO>> UpdateStock(int id, [FromBody] int stock)
        {
            try
            {
                var pizza = await _pizzaService.UpdateStock(id, stock);
                return Ok(_mapper.Map<PizzaDTO>(pizza));
            }
            catch (NoSuchPizzaException ex)
            {
                return NotFound($"Unable to update Stock : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetPizzaByName")]
        [ProducesResponseType(typeof(PizzaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<PizzaDTO>>> GetBySpeciality(string name)
        {
            try
            {
                var pizzas = await _pizzaService.GetPizzaByName(name);
                return Ok(_mapper.Map<IList<PizzaDTO>>(pizzas));
            }
            catch (NoPizzasFoundException ex)
            {
                return NotFound($"Unable to Fetch Pizzas: {ex.Message}");
            }
        }

    }
}

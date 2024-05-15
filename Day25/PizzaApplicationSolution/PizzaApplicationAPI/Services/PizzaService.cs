using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;
using System.Xml.Linq;

namespace PizzaApplicationAPI.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IRepository<int, Pizza> _pizzaRepo;

        public PizzaService(IRepository<int, Pizza> pizzaRepo)
        {
            _pizzaRepo = pizzaRepo;
        }

        public async Task<PizzaDTO> AddPizza(PizzaDTO pizzaDto)
        {
            try
            {
                if (pizzaDto == null)
                {
                    throw new NoSuchPizzaException();
                }
                Pizza pizza = MapPizzaDTOToPizza(pizzaDto);
                var addedPizza = await _pizzaRepo.Add(pizza);
                return pizzaDto;
            }
            catch(Exception ex)
            {
                throw new NoSuchPizzaException();
            }
        }

        public async Task<PizzaDTO> DeletePizzaById(int id)
        {
            var pizza = await _pizzaRepo.Get(id);
            if (pizza == null)
                throw new NoPizzasFoundException();
            pizza = await _pizzaRepo.Delete(id);
            return MapPizzaToPizzaDTO(pizza);
        }

        public async Task<IEnumerable<PizzaDTO>> GetAll()
        {
            try
            {
                var pizzasInStock = (await _pizzaRepo.GetAll()).Where(p => p.Stock > 0).ToList();
                if(!(pizzasInStock.Any()))
                {
                    throw new NoPizzasFoundException();
                }
                IList<PizzaDTO> pizzaDTOs = new List<PizzaDTO>();
                foreach(var pizza in pizzasInStock)
                {
                    pizzaDTOs.Add(MapPizzaToPizzaDTO(pizza));
                }
                return pizzaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PizzaDTO>> GetPizzaByName(string name)
        {
            try
            {
                var pizzasInStock = (await _pizzaRepo.GetAll()).Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
                if (!(pizzasInStock.Any()))
                {
                    throw new NoPizzasFoundException();
                }
                IList<PizzaDTO> pizzaDTOs = new List<PizzaDTO>();
                foreach (var pizza in pizzasInStock)
                {
                    pizzaDTOs.Add(MapPizzaToPizzaDTO(pizza));
                }
                return pizzaDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<PizzaDTO> UpdateStock(int id, int stock)
        {
            var pizza = await _pizzaRepo.Get(id);
            if (pizza == null)
                throw new NoPizzasFoundException();
            pizza.Stock = stock;
            pizza = await _pizzaRepo.Update(pizza);
            return MapPizzaToPizzaDTO(pizza);
        }

        private Pizza MapPizzaDTOToPizza(PizzaDTO pizzaDTO)
        {
            Pizza pizza = new Pizza();
            pizza.Name = pizzaDTO.Name;
            pizza.Stock = pizzaDTO.Stock;
            pizza.Price = pizzaDTO.Price;
            pizza.Description = pizzaDTO.Description;
            return pizza;
        }


        private PizzaDTO MapPizzaToPizzaDTO(Pizza pizza)
        {
            PizzaDTO pizzaDto = new PizzaDTO();
            pizzaDto.Name = pizza.Name ;
            pizzaDto.Stock = pizza.Stock;
            pizzaDto.Price = pizza.Price;
            pizzaDto.Description = pizza.Description;
            return pizzaDto;
        }

    }
}

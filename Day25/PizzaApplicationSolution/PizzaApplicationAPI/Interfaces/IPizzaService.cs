using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Interfaces
{
    public interface IPizzaService
    {
        public Task<PizzaDTO> AddPizza(PizzaDTO pizzaDto);
        public Task<PizzaDTO> UpdateStock(int id, int stock);
        public Task<IEnumerable<PizzaDTO>> GetAll();
        public Task<IEnumerable<PizzaDTO>> GetPizzaByName(string name);
        public Task<PizzaDTO> DeletePizzaById(int id);
    }
}

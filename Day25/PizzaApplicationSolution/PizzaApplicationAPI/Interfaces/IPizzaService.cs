using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Models.DTOs;

namespace PizzaApplicationAPI.Interfaces
{
    public interface IPizzaService
    {
        public Task<Pizza> AddPizza(Pizza pizza);
        public Task<Pizza> UpdateStock(int id, int stock);
        public Task<IEnumerable<Pizza>> GetAll();
        public Task<IEnumerable<Pizza>> GetPizzaByName(string name);
        public Task<Pizza> DeletePizzaById(int id);
    }
}

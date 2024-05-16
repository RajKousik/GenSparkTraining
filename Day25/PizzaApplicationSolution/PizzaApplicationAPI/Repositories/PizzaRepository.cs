using Microsoft.EntityFrameworkCore;
using PizzaApplicationAPI.Contexts;
using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Exceptions.PizzaExceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;


namespace PizzaApplicationAPI.Repositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        private readonly PizzaDbContext _context;
        public PizzaRepository(PizzaDbContext context)
        {
            _context = context;
        }
        public async Task<Pizza> Add(Pizza item)
        {
            if (item == null)
            {
                throw new NoSuchPizzaException();
            }
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Pizza> Delete(int key)
        {
            var pizza = await Get(key);
            if (pizza != null)
            {
                _context.Remove(pizza);
                _context.SaveChanges();
                return pizza;
            }
            throw new NoSuchPizzaException();
        }

        public async Task<Pizza> Get(int key)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(e => e.Id == key);
            if (pizza == null)
            {
                throw new NoSuchPizzaException();
            }
            return pizza;
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            var pizzas = await _context.Pizzas.ToListAsync();
            if (pizzas.Count == 0)
            {
                throw new NoPizzasFoundException();
            }
            return pizzas;

        }

        public async Task<Pizza> Update(Pizza item)
        {
            var pizza = await Get(item.Id);
            if (pizza != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return pizza;
            }
            throw new NoSuchPizzaException();
        }
    }
}

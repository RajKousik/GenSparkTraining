using Microsoft.EntityFrameworkCore;
using PizzaApplicationAPI.Contexts;
using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Repositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        private readonly PizzaDbContext _context;
        public OrderRepository(PizzaDbContext context)
        {
            _context = context;
        }
        public async Task<Order> Add(Order item)
        {
            if (item == null)
            {
                throw new NoSuchOrderException();
            }
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Order> Delete(int key)
        {
            var order = await Get(key);
            if (order != null)
            {
                _context.Remove(order);
                _context.SaveChanges();
                return order;
            }
            throw new NoSuchOrderException();
        }

        public async Task<Order> Get(int key)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.Id == key);
            if (order == null)
            {
                throw new NoSuchOrderException();
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            if (orders.Count == 0)
            {
                throw new NoOrdersFoundException();
            }
            return orders;

        }

        public async Task<Order> Update(Order item)
        {
            var order = await Get(item.Id);
            if (order != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return order;
            }
            throw new NoSuchOrderException();
        }
    }
}

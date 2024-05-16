using Microsoft.EntityFrameworkCore;
using PizzaApplicationAPI.Contexts;
using PizzaApplicationAPI.Exceptions;
using PizzaApplicationAPI.Exceptions.UserExceptions;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;

namespace PizzaApplicationAPI.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly PizzaDbContext _context;
        public UserRepository(PizzaDbContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            if(item == null)
            {
                throw new NoSuchUserException();
            }
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return user;
            }
            throw new NoSuchUserException();
        }


        public async Task<User> Get(int key)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Id == key);
            if(user == null)
            {
                throw new NoSuchUserException();
            }
            return user;
        }


        

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if(users.Count == 0)
            {
                throw new NoUsersFoundException();
            }
            return users;

        }

        public async Task<User> Update(User item)
        {
            var employee = await Get(item.Id);
            if (employee != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return employee;
            }
            throw new NoSuchUserException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Contexts;
using ProductWebAPI.Interfaces;
using ProductWebAPI.Models;

namespace ProductWebAPI.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly ProductWebAPIContext _context;

        public ProductRepository(ProductWebAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}

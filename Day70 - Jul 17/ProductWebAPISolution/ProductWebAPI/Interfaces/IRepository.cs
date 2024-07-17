using ProductWebAPI.Models;

namespace ProductWebAPI.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}

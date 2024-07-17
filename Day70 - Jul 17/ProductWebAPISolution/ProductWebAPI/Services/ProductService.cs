using ProductWebAPI.Interfaces;
using ProductWebAPI.Models;
using ProductWebAPI.Repository;

namespace ProductWebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _productRepository;

        public ProductService(IRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
    }
}

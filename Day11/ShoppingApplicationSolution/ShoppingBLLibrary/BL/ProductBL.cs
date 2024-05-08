using ShoppingApplicationModelLibrary;
using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.BL
{
    public class ProductBL : IProductService
    {
        readonly IRepository<int, Product> _productRepository;


        [ExcludeFromCodeCoverage]
        public ProductBL()
        {
            _productRepository = new ProductRepository();
        }



        [ExcludeFromCodeCoverage]
        public ProductBL(IRepository<int, Product> productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<int> AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(product.Name);
            var existingProduct = await _productRepository.GetAll();
            var isProductExists = existingProduct.Any(p => p.Id == product.Id);
            if (isProductExists)
            {
                throw new ArgumentException("Product with the same Id Already Exists");
            }

            var result = _productRepository.Add(product);
            if (result != null)
            {
                return result.Id;
            }
            throw new NoProductWithGivenIdException();

        }
        [ExcludeFromCodeCoverage]
        public async Task<Product> GetProductByName(string name)
        {
            var product = await _productRepository.GetAll();
            var productToBeReturned = product.ToList().Find(e => e.Name == name);
            if (productToBeReturned == null)
            {
                throw new NoProductWithGivenIdException();
            }
            return productToBeReturned;
        }
        public async Task<Product> DeleteProduct(int id)
        {
            var result = await _productRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var result = await _productRepository.GetAll();
            if (result.Count != 0)
            {
                return result.ToList();
            }
            throw new NoProductWithGivenIdException();

        }

        public async Task<Product> GetProductById(int id)
        {
            var result = await _productRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _productRepository.Update(product);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }
    }
}
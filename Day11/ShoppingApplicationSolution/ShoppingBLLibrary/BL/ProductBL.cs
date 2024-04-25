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
        public int AddProduct(Product product)
        {
            var result = _productRepository.Add(product);
            if (result != null)
            {
                return result.Id;
            }
            throw new NoProductWithGivenIdException();

        }

        public Product GetProductByName(string name)
        {
            var product = _productRepository.GetAll().ToList().Find(e => e.Name == name);
            if (product == null)
            {
                throw new NoProductWithGivenIdException();
            }
            return product;
        }
        public Product DeleteProduct(int id)
        {
            var result = _productRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }

        public List<Product> GetAllProducts()
        {
            var result = _productRepository.GetAll();
            if (result.Count != 0)
            {
                return result.ToList();
            }
            throw new NoProductWithGivenIdException();

        }

        public Product GetProductById(int id)
        {
            var result = _productRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }

        public Product UpdateProduct(Product product)
        {
            var result = _productRepository.Update(product);
            if (result != null)
            {
                return result;
            }
            throw new NoProductWithGivenIdException();

        }
    }
}

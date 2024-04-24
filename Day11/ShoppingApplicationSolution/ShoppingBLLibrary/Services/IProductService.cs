using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.Services
{
    public interface IProductService
    {
        int AddProduct(Product product);
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        Product UpdateProduct(Product product);
        Product DeleteProduct(int id);
    }
}

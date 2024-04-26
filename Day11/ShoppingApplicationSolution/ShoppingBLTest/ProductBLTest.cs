using ShoppingApplicationModelLibrary;
using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingBLLibrary.BL;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLTest
{
    public class ProductBLTest
    {
        IRepository<int, Product> repository;
        IProductService productService;
        [SetUp]
        public void Setup()
        {
            repository = new ProductRepository();
            Product product = new Product() { Id = 10, Name = "Nike Shoe", Price = 500, QuantityInHand = 10 };
            productService = new ProductBL(repository);
            productService.AddProduct(product);
        }


        [Test]
        public void GetByKeySuccessTest()
        {
            var product = productService.GetProductById(1);
            Assert.AreEqual(1, product.Id);
        }

        [Test]
        public void GetByKeyFailureTest()
        {
            Assert.Throws<NoProductWithGivenIdException>(() => productService.GetProductById(3));
        }

        [Test]
        public void AddCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 2, Name = "Pen", Price = 20, QuantityInHand = 4 };

            // Action
            var addedItemId = productService.AddProduct(product);

            Assert.AreEqual(2, addedItemId.Result);
        }

        [Test]
        public void AddCartItemFailureTest()
        {
            // Arrange
            Product product = null; // Provide invalid cart item data

            // Action and Assert
            Assert.Throws<NullReferenceException>(() => productService.AddProduct(product));
        }

        [Test]
        public void DeleteCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 3, Name = "Eraser", Price = 5, QuantityInHand = 3 };

            var addedItemId = productService.AddProduct(product);

            // Action
            var deletedItem = productService.DeleteProduct(addedItemId.Result);

            // Assert
            Assert.IsNotNull(deletedItem);
        }

        [Test]
        public void DeleteCartItemFailureTest()
        {
            // Action and Assert
            Assert.Throws<NoProductWithGivenIdException>(() => productService.DeleteProduct(1000));
        }

        [Test]
        public void GetAllCartItemsSuccessTest()
        {
            // Action
            var products = productService.GetAllProducts();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsNotEmpty(products.Result);
        }

        [Test]
        public void GetAllCartItemsFailureTest()
        {
            // Arrange
            // Clear existing cart items (if any)
            productService.DeleteProduct(1);

            // Action and Assert
            Assert.Throws<NoProductWithGivenIdException>(()=> productService.GetAllProducts());
        }


        [Test]
        public void UpdateCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 4, Name = "Scale", Price = 15.00, QuantityInHand = 20 };

            var addedItemId = productService.AddProduct(product);

            product.Price = 10.00; // Update quantity

            // Action
            var updatedItem = productService.UpdateProduct(product);

            // Assert
            Assert.AreEqual(updatedItem.Result.Price, 10.00);
        }


        [Test]
        public void UpdateCartItemFailureTest()
        {
            // Arrange
            Product product = null; // Provide invalid cart item data

            // Action and Assert
            Assert.Throws<NullReferenceException>(() => productService.UpdateProduct(product));
        }
    }
}

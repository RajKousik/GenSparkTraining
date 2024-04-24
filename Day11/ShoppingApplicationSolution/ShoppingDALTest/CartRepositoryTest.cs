using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDALTest
{
    public class CartRepositoryTest
    {
        IRepository<int, Cart> repository;

        [SetUp]
        public void Setup()
        {
            repository = new CartRepository();

            Customer customer = new Customer() { Id = 101, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 1 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 1, Customer = customer, CustomerId = 101, TotalPrice = 100, CartItems = cartItems };
            repository.Add(cart);

        }

        // ADD
        [Test]
        public void AddSuccessTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 2, Customer = customer, CustomerId = 102, TotalPrice = 100, CartItems = cartItems };

            var result = repository.Add(cart);

            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void AddFailTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 2, Customer = customer, CustomerId = 201, TotalPrice = 100, CartItems = cartItems };

            var result = repository.Add(cart);
            Assert.AreNotEqual(102, result.CustomerId);
        }

        // GET BY ID
        [Test]
        public void GetSuccessTest()
        {
            // Arrange
            int cartId = 1;

            // Act
            var result = repository.GetByKey(cartId);

            // Assert
            Assert.AreEqual(cartId, result.Id);
        }

        [Test]
        public void GetFailTest()
        {
            // Arrange
            int cartId = 999; // Non-existent cartId

            // Assert
            Assert.Throws<NoCartWithGivenIdException>(() => repository.GetByKey(cartId));
        }

        // GET ALL
        [Test]
        public void GetAllSuccessTest()
        {
            // Act
            var result = repository.GetAll();

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetAllFailTest()
        {
            var delete = repository.Delete(1);
            var result = repository.GetAll();

            Assert.IsEmpty(result);
        }

        // UPDATE
        [Test]
        public void UpdateSuccessTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 2, Customer = customer, CustomerId = 102, TotalPrice = 200, CartItems = cartItems };
            var addedCart = repository.Add(cart);

            // Act
            var result = repository.Update(cart);

            // Assert
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public void UpdateFailTest()
        {
            // Arrange
            int cartId = 1;
            var cart = repository.GetByKey(cartId);
            var result = repository.Delete(cartId);

            // Assert
            Assert.Throws<NoCartWithGivenIdException>(() => repository.Update(cart)); ;
        }

        //DELETE
        [Test]
        public void DeleteSuccessTest()
        {
            // Arrange
            int cartId = 1;

            // Act
            var result = repository.Delete(cartId);

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void DeleteFailTest()
        {
            // Arrange
            int cartId = 999; // Non-existent cartId

            // Assert
            Assert.Throws<NoCartWithGivenIdException>(() => repository.Delete(cartId));
        }

    }
}

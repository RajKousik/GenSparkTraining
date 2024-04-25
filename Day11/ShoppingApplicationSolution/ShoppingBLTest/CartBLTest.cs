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
    public class CartBLTest
    {
        IRepository<int, Cart> repository;
        ICartService cartService;
        [SetUp]
        public void Setup()
        {
            repository = new CartRepository();
            Customer customer = new Customer() { Id = 101, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 1 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 1, Customer = customer, CustomerId = 101, TotalPrice = 100, CartItems = cartItems };
            repository.Add(cart);
            cartService = new CartBL(repository);
        }

        // GET
        [Test]
        public void AddSuccessTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 2, Customer = customer, CustomerId = 102, TotalPrice = 100, CartItems = cartItems };

            var result = cartService.AddCart(cart);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddFailureTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 2, Customer = customer, CustomerId = 111, TotalPrice = 100, CartItems = cartItems };

            var result = cartService.AddCart(cart);

            Assert.AreNotEqual(102, result);
        }

        [Test]
        public void GetAllSuccessTest()
        {
            var result = cartService.GetAllCarts();
            Assert.AreEqual(1, result.Count);
        }
        [Test]
        public void GetAllFailureTest()
        {
            var deletedCart = cartService.DeleteCart(1);

            Assert.Throws<CartNotFoundException>(() => cartService.GetAllCarts());
        }


        // GET BY KEY

        [Test]
        public void GetByKeySuccessTest()
        {
            int cartId = 1;

            var result = cartService.GetCartById(cartId);

            Assert.AreEqual(cartId, result.Id);
        }


        [Test]
        public void GetByKeyFailureTest()
        {
            Assert.Throws<NoCartWithGivenIdException>(() => cartService.GetCartById(999));
        }

        // DELETE

        [Test]
        public void DeleteSuccessTest()
        {
            var result = cartService.DeleteCart(1);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void DeleteFailureTest()
        {
            Assert.Throws<NoCartWithGivenIdException>(() => cartService.DeleteCart(2));
        }


        [Test]
        public void UpdateSuccessTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 1, Customer = customer, CustomerId = 102, TotalPrice = 5000, CartItems = cartItems };

            var result = cartService.UpdateCart(cart);

            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void UpdateFailureTest()
        {
            Customer customer = new Customer() { Id = 102, Age = 25, Phone = "1234556789" };
            CartItem cartItem = new CartItem() { CartId = 2 };
            List<CartItem> cartItems = new List<CartItem>() { cartItem };
            Cart cart = new Cart() { Id = 4, Customer = customer, CustomerId = 102, TotalPrice = 5000, CartItems = cartItems };

            Assert.Throws<NoCartWithGivenIdException>(() => cartService.UpdateCart(cart));
        }

    }
}

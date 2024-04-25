using ShoppingApplicationModelLibrary;
using ShoppingBLLibrary.Services;
using ShoppingBLLibrary.BL;
using ShoppingDALLibrary;
using ShoppingApplicationModelLibrary.Exceptions;

namespace ShoppingBLTest
{
    public class CartItemBLTest
    {

        IRepository<int, CartItem> repository;
        ICartItemService cartItemService;
        [SetUp]
        public void Setup()
        {
            repository = new CartItemRepository();
            Product product = new Product() { Id=1, Name="Pencil", Price=50, QuantityInHand=10};
            CartItem cartItem = new CartItem() {CartId=1, Discount=10, Price=50, PriceExpiryDate=DateTime.Now, ProductId =1, Quantity=4, Product=product};
            
            cartItemService = new CartItemBL(repository);
            cartItemService.AddCartItem(cartItem);
        }


        [Test]
        public void GetByKeySuccessTest()
        {
            var cartItem = cartItemService.GetCartItemById(1);
            Assert.AreEqual(1, cartItem.CartId);
        }

        [Test]
        public void GetByKeyFailureTest()
        {
            Assert.Throws<NoCartItemWithGivenIdException>(() => cartItemService.GetCartItemById(3));
        }

        [Test]
        public void AddCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 1, Name = "Pencil", Price = 50, QuantityInHand = 10 };
            CartItem cartItem = new CartItem() { CartId = 1, Discount = 10, Price = 50, PriceExpiryDate = DateTime.Now, ProductId = 1, Quantity = 4, Product = product };

            // Action
            int addedItemId = cartItemService.AddCartItem(cartItem);
            var addedItem = cartItemService.GetCartItemById(addedItemId);

            // Assert
            Assert.IsNotNull(addedItem);
        }

        [Test]
        public void AddCartItemFailureTest()
        {
            // Arrange
            CartItem cartItem = null; // Provide invalid cart item data

            // Action and Assert
            Assert.Throws<NullReferenceException>(() => cartItemService.AddCartItem(cartItem));
        }

        [Test]
        public void DeleteCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 1, Name = "Pencil", Price = 50, QuantityInHand = 10 };
            CartItem cartItem = new CartItem() { CartId = 1, Discount = 10, Price = 50, PriceExpiryDate = DateTime.Now, ProductId = 1, Quantity = 4, Product = product };
            int addedItemId = cartItemService.AddCartItem(cartItem);

            // Action
            var deletedItem = cartItemService.DeleteCartItem(addedItemId);

            // Assert
            Assert.IsNotNull(deletedItem);
        }

        [Test]
        public void DeleteCartItemFailureTest()
        {
            // Action and Assert
            Assert.Throws<NoCartItemWithGivenIdException>(() => cartItemService.DeleteCartItem(1000));
        }

        [Test]
        public void GetAllCartItemsSuccessTest()
        {
            // Action
            var cartItems = cartItemService.GetAllCartItems();

            // Assert
            Assert.IsNotNull(cartItems);
            Assert.IsNotEmpty(cartItems);
        }

        [Test]
        public void GetAllCartItemsFailureTest()
        {
            // Arrange
            // Clear existing cart items (if any)
            cartItemService.DeleteCartItem(1);
            

            // Action and Assert
            Assert.Throws<NoCartItemWithGivenIdException>(() => cartItemService.GetAllCartItems());
        }


        [Test]
        public void UpdateCartItemSuccessTest()
        {
            // Arrange
            Product product = new Product() { Id = 2, Name = "Pen", Price = 50, QuantityInHand = 10 };
            CartItem cartItem = new CartItem() { CartId = 1, Discount = 10, Price = 50, PriceExpiryDate = DateTime.Now, ProductId = 2, Quantity = 4, Product = product };
            int addedItemId = cartItemService.AddCartItem(cartItem);
            cartItem.CartId = addedItemId;
            cartItem.Quantity = 2; // Update quantity

            // Action
            var updatedItem = cartItemService.UpdateCartItem(cartItem);

            // Assert
            Assert.AreEqual(cartItem.Quantity, updatedItem.Quantity);
        }


        [Test]
        public void UpdateCartItemFailureTest()
        {
            // Arrange
            CartItem cartItem = null; // Provide invalid cart item data

            // Action and Assert
            Assert.Throws<NullReferenceException>(() => cartItemService.UpdateCartItem(cartItem));
        }
    }
}
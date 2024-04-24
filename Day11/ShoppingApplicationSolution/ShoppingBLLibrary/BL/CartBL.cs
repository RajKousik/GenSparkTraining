using ShoppingApplicationModelLibrary;
using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System.Numerics;
using System.Runtime.ConstrainedExecution;

namespace ShoppingBLLibrary.BL
{
    public class CartBL : ICartService
    {
        private const double SHIPPING_CHARGE = 100.00;
        private const double DISCOUNT_PERCENTAGE = 0.05;
        private readonly IRepository<int, Cart> _cartRepository;

        public CartBL()
        {
            _cartRepository = new CartRepository();
        }

        public CartBL(IRepository<int, Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public int AddCart(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }

            // Apply shipping charges, discounts, and check maximum quantities
            ProccessCart(cart);

            // Add the cart to the repository
            Cart addedCart = _cartRepository.Add(cart);
            return addedCart.Id;
        }

        public Cart DeleteCart(int id)
        {
            Cart deletedCart = _cartRepository.Delete(id);
            if (deletedCart == null)
            {
                throw new CartNotFoundException();
            }
            return deletedCart;
        }

        public List<Cart> GetAllCarts()
        {
            List<Cart> carts = _cartRepository.GetAll().ToList();
            if (carts.Count == 0)
            {
                throw new CartNotFoundException();
            }
            return carts;
        }

        public Cart GetCartById(int id)
        {
            Cart cart = _cartRepository.GetByKey(id);
            if (cart == null)
            {
                throw new CartNotFoundException();
            }
            return cart;
        }

        public Cart UpdateCart(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }

            // Apply shipping charges, discounts, and check maximum quantities
            ProccessCart(cart);

            // Update the cart in the repository
            Cart updatedCart = _cartRepository.Update(cart);
            if (updatedCart == null)
            {
                throw new CartNotFoundException();
            }
            return updatedCart;
        }

        private void ProccessCart(Cart cart)
        {
            // Calculate total price by summing up prices of cart items
            double totalPrice = 0;
            foreach (var item in cart.CartItems)
            {
                totalPrice += item.Price * item.Quantity;
            }

            // Add shipping charge if total price is less than 100
            if (totalPrice < 100)
            {
                totalPrice += SHIPPING_CHARGE; // Add shipping charge
            }

            // Apply discount if cart has 3 items and total price is 1500 or more
            if (cart.CartItems.Count == 3 && totalPrice >= 1500)
            {
                totalPrice -= totalPrice * DISCOUNT_PERCENTAGE; // Apply 5% discount
            }

            // Check maximum quantity of any product in the cart
            foreach (var item in cart.CartItems)
            {
                if (item.Quantity > 5)
                {
                    throw new ArgumentException($"Maximum quantity of products in cart cannot be more than 5.");
                }
            }

            // Update the total price in the cart
            cart.TotalPrice = totalPrice;
        }
    }
}

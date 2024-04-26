using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingBLLibrary.BL
{
    public class CartItemBL : ICartItemService
    {
        
        readonly IRepository<int, CartItem> _cartItemRepository;
        [ExcludeFromCodeCoverage]
        public CartItemBL()
        {
            _cartItemRepository = new CartItemRepository();
        }

        [ExcludeFromCodeCoverage]
        public CartItemBL(IRepository<int, CartItem> cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<int> AddCartItem(CartItem cartItem)
        {
            if(cartItem == null)
            {
                throw new NullReferenceException();
            }
            ProcessCartItem(cartItem);
            CartItem result = await _cartItemRepository.Add(cartItem);
            if (result != null)
            {
                return result.ProductId;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public async Task<CartItem> DeleteCartItem(int id)
        {
            var deletedCartItem = await _cartItemRepository.Delete(id);
            if (deletedCartItem != null)
            {
                return deletedCartItem;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            var result = await _cartItemRepository.GetAll();
            if (result.Count != 0)
            {
                return (List<CartItem>)result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
            var result = await _cartItemRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new NullReferenceException();
            }
            ProcessCartItem(cartItem);
            var result = await _cartItemRepository.Update(cartItem);
            if (result != null)
            {
                return result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        [ExcludeFromCodeCoverage]
        public void ProcessCartItem(CartItem cartItem)
        {
            cartItem.Price = cartItem.Quantity * cartItem.Product.Price;

            if (cartItem.Quantity > 5)
            {
                throw new ArgumentException("Maximum quantity of product in cart should be less than 5");
            }
        }
    }

}

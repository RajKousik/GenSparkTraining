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

        public int AddCartItem(CartItem cartItem)
        {
            if(cartItem == null)
            {
                throw new NullReferenceException();
            }
            ProcessCartItem(cartItem);
            CartItem result = _cartItemRepository.Add(cartItem);
            if (result != null)
            {
                return result.ProductId;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public CartItem DeleteCartItem(int id)
        {
            var result = _cartItemRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public List<CartItem> GetAllCartItems()
        {
            var result = _cartItemRepository.GetAll();
            if (result.Count != 0)
            {
                return (List<CartItem>)result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public CartItem GetCartItemById(int id)
        {
            var result = _cartItemRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public CartItem UpdateCartItem(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new NullReferenceException();
            }
            ProcessCartItem(cartItem);
            var result = _cartItemRepository.Update(cartItem);
            if (result != null)
            {
                return result;
            }
            throw new NoCartItemWithGivenIdException();
        }

        [ExcludeFromCodeCoverage]
        public void ProcessCartItem(CartItem cartItem)
        {
            if (cartItem.Quantity > 5)
            {
                throw new ArgumentException("Maximum quantity of product in cart should be less than 5");
            }
        }
    }

}

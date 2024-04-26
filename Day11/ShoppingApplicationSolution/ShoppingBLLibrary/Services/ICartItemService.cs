using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.Services
{
    public interface ICartItemService
    {
        Task<int> AddCartItem(CartItem cartItem);
        Task<CartItem> GetCartItemById(int id);
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem> UpdateCartItem(CartItem cartItem);
        Task<CartItem> DeleteCartItem(int id);
    }
}

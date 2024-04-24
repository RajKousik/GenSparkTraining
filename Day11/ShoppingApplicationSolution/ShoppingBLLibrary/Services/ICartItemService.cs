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
        int AddCartItem(CartItem cartItem);
        CartItem GetCartItemById(int id);
        List<CartItem> GetAllCartItems();
        CartItem UpdateCartItem(CartItem cartItem);
        CartItem DeleteCartItem(int id);
    }
}

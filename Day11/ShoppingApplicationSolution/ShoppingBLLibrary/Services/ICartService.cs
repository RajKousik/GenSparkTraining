using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.Services
{
    public interface ICartService
    {
        Task<int> AddCart(Cart cart);
        Task<Cart> GetCartById(int id);
        Task<List<Cart>> GetAllCarts();
        Task<Cart> UpdateCart(Cart cart);
        Task<Cart> DeleteCart(int id);
    }
}

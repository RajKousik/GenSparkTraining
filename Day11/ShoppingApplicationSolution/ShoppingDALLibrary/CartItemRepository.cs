
using ShoppingApplicationModelLibrary;
using ShoppingApplicationModelLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDALLibrary
{
    public class CartItemRepository : AbstractRepository<int, CartItem>
    {
        public int GenerateId()
        {
            if (items.Count == 0) return 1;

            int id = items.Count();
            return ++id;
        }

        public override CartItem Add(CartItem item)
        {
            item.CartItemId = GenerateId();
            return base.Add(item);
        }

        public override CartItem Delete(int key)
        {
            CartItem cartItem = GetByKey(key);
            if (cartItem != null)
            {
                items.Remove(cartItem);
                return cartItem;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public override CartItem GetByKey(int key)
        {
            CartItem cartItem = items.ToList().Find(p => p.CartId == key);
            if (cartItem != null)
            {
                return cartItem;
            }
            throw new NoCartItemWithGivenIdException();
        }

        public override CartItem Update(CartItem item)
        {
            CartItem cartItem = GetByKey(item.CartId);
            if (cartItem != null)
            {
                cartItem = item;
                return cartItem;
            }
            throw new NoCartItemWithGivenIdException();
        }


    }

}

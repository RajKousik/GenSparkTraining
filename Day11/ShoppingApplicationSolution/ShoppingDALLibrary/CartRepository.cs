using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDALLibrary
{
    public class CartRepository : AbstractRepository<int, Cart>
    {
        public int GenerateId()
        {
            if (items.Count == 0) return 1;

            int id = items.Count();
            return ++id;
        }

        public override Cart Add(Cart item)
        {
            item.Id = GenerateId();
            return base.Add(item);
        }

        public override Cart Delete(int key)
        {
            Cart cart = GetByKey(key);
            if (cart != null)
            {
                items.Remove(cart);
                return cart;
            }
            throw new NoCartWithGivenIdException();
        }

        public override Cart GetByKey(int key)
        {

            Cart cart = items.ToList().Find((c) => c.Id == key);
            if (cart != null)
            {
                return cart;
            }
            throw new NoCartWithGivenIdException();
        }

        public override Cart Update(Cart item)
        {
            Cart cart = GetByKey(item.Id);
            if (cart != null)
            {
                cart = item;
                return cart;
            }
            throw new NoCartWithGivenIdException();
        }
    }
}

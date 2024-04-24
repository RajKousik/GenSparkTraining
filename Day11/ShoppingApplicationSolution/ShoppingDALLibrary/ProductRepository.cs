using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDALLibrary
{
    public class ProductRepository : AbstractRepository<int, Product>
    {
        public override Product Delete(int key)
        {
            Product product = GetByKey(key);
            if (product != null)
            {
                items.Remove(product);
            }
            return product;
        }

        public override Product GetByKey(int key)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Id == key)
                    return items[i];
            }
            throw new NoProductWithGivenIdException();
        }

        public override Product Update(Product item)
        {
            Product product = GetByKey(item.Id);
            if (product != null)
            {
                product = item;
            }
            return product;
        }
    }
}

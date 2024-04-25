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
        public int GenerateId()
        {
            if (items.Count == 0) return 1;

            int id = items.Count();
            return ++id;
        }

        public override Product Add(Product item)
        {
            item.Id = GenerateId();
            return base.Add(item);
        }

        public override Product Delete(int key)
        {
            Product product = GetByKey(key);
            if (product != null)
            {
                items.Remove(product);
                return product;
            }
            throw new NoProductWithGivenIdException();
        }

        public override Product GetByKey(int key)
        {
            //Predicate<Product> FindPredicate = (p) => p.Id == key;
            //return items.ToList().Find(FindPredicate);
            //return items.ToList().Find((p) => p.Id == key);

            Product product = items.ToList().Find((p) => p.Id == key);
            if(product != null)
            {
                return product;
            }
            throw new NoProductWithGivenIdException();
        }

        public override Product Update(Product item)
        {
            Product product = GetByKey(item.Id);
            if (product != null)
            {
                product = item;
                return product;
            }
            throw new NoProductWithGivenIdException();
        }
    }
}

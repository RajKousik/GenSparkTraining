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

        public override async Task<Product> Add(Product item)
        {
            if(item == null)
            {
                throw new NullReferenceException();
            }
            item.Id = GenerateId();
            return base.Add(item).Result;
        }

        public override async Task<Product> Delete(int key)
        {
            Product product = await GetByKey(key);
            if (product != null)
            {
                items.Remove(product);
                return product;
            }
            throw new NoProductWithGivenIdException();
        }

        public override async Task<Product> GetByKey(int key)
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

        public override async Task<Product> Update(Product item)
        {
            Product product = await GetByKey(item.Id);
            if (product != null)
            {
                product = item;
                return product;
            }
            throw new NoProductWithGivenIdException();
        }
    }
}

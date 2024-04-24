using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDALLibrary
{
    public interface IRepository<K, T>
    {
        T Add(T item);
        T GetByKey(K key);
        ICollection<T> GetAll();
        T Update(T item);
        T Delete(K key);

    }
}

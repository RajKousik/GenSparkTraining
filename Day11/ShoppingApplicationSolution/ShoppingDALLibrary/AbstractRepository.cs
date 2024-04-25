namespace ShoppingDALLibrary
{
    public abstract class AbstractRepository<K, T> : IRepository<K, T>
    {
        protected List<T> items = new List<T>();
        public virtual T Add(T item)
        {
            items.Add(item);
            return item;
        }
        public virtual ICollection<T> GetAll()
        {
            //items.Sort();
            return items;
        }

        public abstract T Delete(K key);



        public abstract T GetByKey(K key);

        public abstract T Update(T item);

    }
}

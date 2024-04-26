namespace ShoppingDALLibrary
{
    public abstract class AbstractRepository<K, T> : IRepository<K, T>
    {
        protected List<T> items = new List<T>();
        public virtual async Task<T> Add(T item)
        {
            items.Add(item);
            return item;
        }
        public virtual async Task<ICollection<T>> GetAll()
        {
            //items.Sort();
            return items;
        }

        public abstract Task<T> Delete(K key);



        public abstract Task<T> GetByKey(K key);

        public abstract Task<T> Update(T item);

    }
}

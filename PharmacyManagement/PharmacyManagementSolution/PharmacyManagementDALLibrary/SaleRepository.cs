using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementDALLibrary
{
    public class SaleRepository : IRepository<int, Sales>
    {
        private readonly Dictionary<int, Sales> _sales;

        public SaleRepository()
        {
            _sales = new Dictionary<int, Sales>();
        }

        private int GenerateId()
        {
            if (_sales.Count == 0)
            {
                return 1;
            }
            int id = _sales.Keys.Max();
            return ++id;
        }

        public Sales Add(Sales item)
        {
            if (_sales.ContainsValue(item))
            {
                return null;
            }
            int id = GenerateId();
            item.SaleId = id;
            _sales.Add(id, item);
            return item;
        }

        public Sales Delete(int key)
        {
            if (_sales.ContainsKey(key))
            {
                var sale = _sales[key];
                _sales.Remove(key);
                return sale;
            }
            return null;
        }

        public Sales Get(int key)
        {
            return _sales.ContainsKey(key) ? _sales[key] : null;
        }

        public List<Sales> GetAll()
        {
            if (_sales.Count == 0)
            {
                return null;
            }
            return _sales.Values.ToList();
        }

        public Sales Update(Sales item)
        {
            if (_sales.ContainsKey(item.SaleId))
            {
                _sales[item.SaleId] = item;
                return item;
            }
            return null;
        }
    }
}

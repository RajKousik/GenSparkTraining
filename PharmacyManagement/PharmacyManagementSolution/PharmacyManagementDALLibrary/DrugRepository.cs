using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementDALLibrary
{
    public class DrugRepository : IRepository<int, Drugs>
    {
        readonly Dictionary<int, Drugs> _drugs;

        public DrugRepository()
        {
            _drugs = new Dictionary<int, Drugs>();
        }

        int GenerateId()
        {
            if (_drugs.Count == 0)
            {
                return 1;
            }
            int id = _drugs.Keys.Max();
            return ++id;
        }

        public Drugs Add(Drugs item)
        {
            if (_drugs.ContainsValue(item))
            {
                return null;
            }
            int id = GenerateId();
            item.Id = id;
            _drugs.Add(id, item);
            return item;
        }

        public Drugs Delete(int key)
        {
            if (_drugs.ContainsKey(key))
            {
                var drug = _drugs[key];
                _drugs.Remove(key);
                return drug;
            }
            return null;
        }

        public Drugs Get(int key)
        {
            return _drugs.ContainsKey(key) ? _drugs[key] : null;
        }

        public List<Drugs> GetAll()
        {
            if (_drugs.Count == 0)
            {
                return null;
            }
            return _drugs.Values.ToList();
        }

        public Drugs Update(Drugs item)
        {
            if (_drugs.ContainsKey(item.Id))
            {
                _drugs[item.Id] = item;
                return item;
            }
            return null;
        }
    }
}

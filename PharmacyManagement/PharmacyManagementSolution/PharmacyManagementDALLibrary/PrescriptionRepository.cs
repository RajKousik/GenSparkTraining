using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementDALLibrary
{
    public class PrescriptionRepository : IRepository<int, Prescription>
    {
        private readonly Dictionary<int, Prescription> _prescriptions;

        public PrescriptionRepository()
        {
            _prescriptions = new Dictionary<int, Prescription>();
        }

        private int GenerateId()
        {
            if (_prescriptions.Count == 0)
            {
                return 1;
            }
            int id = _prescriptions.Keys.Max();
            return ++id;
        }

        public Prescription Add(Prescription item)
        {
            if (_prescriptions.ContainsValue(item))
            {
                return null;
            }
            int id = GenerateId();
            item.Id = id;
            _prescriptions.Add(id, item);
            return item;
        }

        public Prescription Delete(int key)
        {
            if (_prescriptions.ContainsKey(key))
            {
                var prescription = _prescriptions[key];
                _prescriptions.Remove(key);
                return prescription;
            }
            return null;
        }

        public Prescription Get(int key)
        {
            return _prescriptions.ContainsKey(key) ? _prescriptions[key] : null;
        }

        public List<Prescription> GetAll()
        {
            if (_prescriptions.Count == 0)
            {
                return null;
            }
            return _prescriptions.Values.ToList();
        }

        public Prescription Update(Prescription item)
        {
            if (_prescriptions.ContainsKey(item.Id))
            {
                _prescriptions[item.Id] = item;
                return item;
            }
            return null;
        }
    }
}

using PharmacyManagementModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementDALLibrary
{
    public class PatientRepository : IRepository<int, Patient>
    {
        private readonly Dictionary<int, Patient> _patients;

        public PatientRepository()
        {
            _patients = new Dictionary<int, Patient>();
        }

        private int GenerateId()
        {
            if (_patients.Count == 0)
            {
                return 1;
            }
            int id = _patients.Keys.Max();
            return ++id;
        }

        public Patient Add(Patient item)
        {
            if (_patients.ContainsValue(item))
            {
                return null;
            }
            int id = GenerateId();
            item.Id = id;
            _patients.Add(id, item);
            return item;
        }

        public Patient Delete(int key)
        {
            if (_patients.ContainsKey(key))
            {
                var patient = _patients[key];
                _patients.Remove(key);
                return patient;
            }
            return null;
        }

        public Patient Get(int key)
        {
            return _patients.ContainsKey(key) ? _patients[key] : null;
        }

        public List<Patient> GetAll()
        {
            if (_patients.Count == 0)
            {
                return null;
            }
            return _patients.Values.ToList();
        }

        public Patient Update(Patient item)
        {
            if (_patients.ContainsKey(item.Id))
            {
                _patients[item.Id] = item;
                return item;
            }
            return null;
        }
    }
}

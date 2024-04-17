using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDALLibrary
{
    public class PatientRepository : IRepository<int, Patient>
    {
        private readonly Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();

        public Patient Add(Patient item)
        {
            if (_patients.ContainsValue(item))
            {
                return null!;
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
            return null!;
        }

        public Patient Get(int key) => _patients.ContainsKey(key) ? _patients[key] : null!;

        public List<Patient> GetAll() => _patients.Values.ToList();

        public Patient Update(Patient item)
        {
            if (_patients.ContainsKey(item.Id))
            {
                _patients[item.Id] = item;
                return item;
            }
            return null!;
        }

        private int GenerateId()
        {
            return _patients.Count == 0 ? 1 : _patients.Keys.Max() + 1;
        }

    }
}

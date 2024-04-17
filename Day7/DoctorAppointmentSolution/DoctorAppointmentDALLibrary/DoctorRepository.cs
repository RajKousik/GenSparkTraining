using DoctorAppointmentModelLibrary;

namespace DoctorAppointmentDALLibrary
{
    public class DoctorRepository : IRepository<int, Doctor>
    {

        private readonly Dictionary<int, Doctor> _doctors = new Dictionary<int, Doctor>();

        public Doctor Add(Doctor item)
        {
            if (_doctors.ContainsValue(item))
            {
                return null!;
            }

            int id = GenerateId();
            item.Id = id;
            _doctors.Add(id, item);
            return item;
        }

        public Doctor Delete(int key)
        {
            if (_doctors.ContainsKey(key))
            {
                var doctor = _doctors[key];
                _doctors.Remove(key);
                return doctor;
            }
            return null!;
        }

        public Doctor Get(int key) => _doctors.ContainsKey(key) ? _doctors[key] : null!;

        public List<Doctor> GetAll() => _doctors.Values.ToList();

        public Doctor Update(Doctor item)
        {
            if (_doctors.ContainsKey(item.Id))
            {
                _doctors[item.Id] = item;
                return item;
            }
            return null!;
        }

        private int GenerateId()
        {
            return _doctors.Count == 0 ? 1 : _doctors.Keys.Max() + 1;
        }

    }
}

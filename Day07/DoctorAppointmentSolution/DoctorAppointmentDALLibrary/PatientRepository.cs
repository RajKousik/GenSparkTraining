using DoctorAppointmentDALLibrary.Model;



namespace DoctorAppointmentDALLibrary
{
    public class PatientRepository : IRepository<int, Patient>
    {
        dbDoctorAppointmentContext context = new dbDoctorAppointmentContext();
        private List<Patient> _patients;

        public PatientRepository()
        {
            _patients = context.Patients.ToList();
        }

        public Patient Add(Patient item)
        {
            context.Patients.Add(item);
            context.SaveChanges();
            _patients = context.Patients.ToList();
            if (_patients.Contains(item))
                return item;
            return null;
        }

        public Patient Delete(int key)
        {
            var patient = _patients.SingleOrDefault(d => d.Id == key);
            if (patient != null)
            {
                context.Patients.Remove(patient);
                context.SaveChanges();
                _patients = context.Patients.ToList();
                return patient;
            }
            return null!;
        }

        public Patient Get(int key)
        {

            var patient = _patients.SingleOrDefault(d => d.Id == key);
            if (patient != null)
            {
                return patient;
            }
            return null;

        }

        public List<Patient> GetAll()
        {
            var list = context.Patients.ToList();
            if (list.Count == 0)
                return null;

            return list;
        }

        public Patient Update(Patient item)
        {
            var patient = _patients.SingleOrDefault(d => d.Id == item.Id);
            if (patient != null)
            {
                patient = item;
                context.Patients.Update(patient);
                context.SaveChanges();
                return patient;
            }
            return null;
        }

        //private int GenerateId()
        //{
        //    return _patients.Count == 0 ? 1 : _patients.Keys.Max() + 1;
        //}

    }
}

//using DoctorAppointmentModelLibrary;
using DoctorAppointmentDALLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDALLibrary
{
    public class AppointmentRepository : IRepository<int, Appointment>
    {
        dbDoctorAppointmentContext context = new dbDoctorAppointmentContext();
        private List<Appointment> _appointments;
        public AppointmentRepository()
        {
            _appointments = context.Appointments.ToList();
        }

        public Appointment Add(Appointment item)
        {
            context.Appointments.Add(item);
            context.SaveChanges();
            _appointments = context.Appointments.ToList();
            if (_appointments.Contains(item)) return item;
            return null;
        }

        public Appointment Delete(int key)
        {
            _appointments = context.Appointments.ToList();
            var appointment = _appointments.SingleOrDefault(d => d.Id == key);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
                _appointments = context.Appointments.ToList();
                return appointment;
            }
            return null;
        }

        public List<Appointment> GetAll()
        {
            if (_appointments.Count == 0)
                return null;
            return _appointments;
        }

        public Appointment Get(int key)
        {
            var appointment = _appointments.SingleOrDefault(d => d.Id == key);
            if (appointment != null)
            {
                return appointment;
            }
            return null;
        }

        public Appointment Update(Appointment item)
        {
            _appointments = context.Appointments.ToList();
            var appointment = _appointments.SingleOrDefault(d => d.Id == item.Id);
            if (appointment != null)
            {
                appointment = item;
                context.Appointments.Update(appointment);
                context.SaveChanges();
                _appointments = context.Appointments.ToList();
                return appointment;
            }
            return null;
        }


        //private int GenerateId()
        //{
        //    return _appointments.Count == 0 ? 1 : _appointments.Keys.Max() + 1;
        //}
    }

}

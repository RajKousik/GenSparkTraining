using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;

        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public int AddAppointment(Appointment appointment)
        {
            Appointment added = _appointmentRepository.Add(appointment);
            return added != null ? added.Id : -1;
        }

        public Appointment DeleteAppointment(int id)
        {
            return _appointmentRepository.Delete(id);
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _appointmentRepository.Get(id);
        }

        public Appointment UpdateAppointment(Appointment appointment)
        {
            return _appointmentRepository.Update(appointment);
        }
    }

}

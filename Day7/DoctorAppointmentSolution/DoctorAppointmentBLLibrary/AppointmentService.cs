using DoctorAppointmentBLLibrary.Exceptions;
using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<int, Appointment> _appointmentRepository;

        [ExcludeFromCodeCoverage]
        public AppointmentService()
        {
            _appointmentRepository = new AppointmentRepository();
        }

        public AppointmentService(IRepository<int, Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public int AddAppointment(Appointment appointment)
        {
            Appointment added = _appointmentRepository.Add(appointment);
            return added != null ? added.Id : throw new DuplicateAppointmentException();
        }

        public Appointment DeleteAppointment(int id)
        {
            Appointment deleted = _appointmentRepository.Delete(id);
            return deleted != null ? deleted : throw new AppointmentNotFoundException();
        }

        public List<Appointment> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAll();
            if (appointments.Count == 0)
            {
                throw new AppointmentNotFoundException();
            }
            return appointments;
        }

        public Appointment GetAppointmentById(int id)
        {
            var result = _appointmentRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new AppointmentNotFoundException();
        }

        public Appointment UpdateAppointment(Appointment appointment)
        {
            var result = _appointmentRepository.Update(appointment);
            if (result != null)
            {
                return result;
            }
            throw new AppointmentNotFoundException();
        }
    }

}

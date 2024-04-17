using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLLibrary
{
    public interface IAppointmentService
    {
        int AddAppointment(Appointment appointment);
        Appointment GetAppointmentById(int id);
        List<Appointment> GetAllAppointments();
        Appointment UpdateAppointment(Appointment appointment);
        Appointment DeleteAppointment(int id);
    }

}

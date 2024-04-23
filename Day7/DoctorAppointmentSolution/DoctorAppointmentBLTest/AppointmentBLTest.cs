using DoctorAppointmentBLLibrary;
using DoctorAppointmentBLLibrary.Exceptions;
using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBLTest
{
    public class AppointmentBLTest
    {
        IRepository<int, Appointment> repository;
        IAppointmentService appointmentService;
        [SetUp]
        public void Setup()
        {
            repository = new AppointmentRepository();
            Appointment appointment = new Appointment(1,1,DateTime.Now);
            repository.Add(appointment);
            appointmentService = new AppointmentService(repository);
        }

        [Test]
        public void AddAppointment_Success_Test()
        {
            // Arrange
            Appointment newAppointment = new Appointment(2, 2, DateTime.Now);

            // Act
            int result = appointmentService.AddAppointment(newAppointment);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddAppointment_Failure_Test()
        {
            // Arrange
            Appointment existingAppointment = new Appointment() { Id = 1, DateTime = DateTime.Now, DoctorId = 1, PatientId = 1 }; ;

            // Act & Assert
            Assert.Throws<DuplicateAppointmentException>(() => appointmentService.AddAppointment(existingAppointment));
        }

        [Test]
        public void AddAppointment_Exception_Test()
        {
            // Arrange
            Appointment appointment = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => appointmentService.AddAppointment(appointment));
        }


        [Test]
        public void DeleteAppointment_Success_Test()
        {
            // Act
            Appointment deletedAppointment = appointmentService.DeleteAppointment(1);

            // Assert
            Assert.IsNotNull(deletedAppointment);
        }

        [Test]
        public void DeleteAppointment_Failure_Test()
        {
            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.DeleteAppointment(2));
        }

        [Test]
        public void DeleteAppointment_Exception_Test()
        {
            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.DeleteAppointment(0));
        }


        [Test]
        public void GetAllAppointments_Success_Test()
        {
            // Act
            var appointments = appointmentService.GetAllAppointments();

            // Assert
            Assert.IsNotEmpty(appointments);
        }

        [Test]
        public void GetAllAppointments_Failure_Test()
        {
            // Arrange
            repository.Delete(1);

            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.GetAllAppointments());
        }

        [Test]
        public void GetAllAppointments_Exception_Test()
        {
            // Arrange
            repository = null;
            appointmentService = new AppointmentService(repository);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => appointmentService.GetAllAppointments());
        }


        [Test]
        public void GetAppointmentById_Success_Test()
        {
            // Act
            var appointment = appointmentService.GetAppointmentById(1);

            // Assert
            Assert.IsNotNull(appointment);
        }

        [Test]
        public void GetAppointmentById_Failure_Test()
        {
            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.GetAppointmentById(2));
        }

        [Test]
        public void GetAppointmentById_Exception_Test()
        {
            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.GetAppointmentById(0));
        }


        [Test]
        public void UpdateAppointment_Success_Test()
        {
            // Arrange
            Appointment updatedAppointment = new Appointment() { Id=1, DoctorId=1,PatientId=1, DateTime= DateTime.Now.AddDays(1) };
            // Act
            var result = appointmentService.UpdateAppointment(updatedAppointment);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateAppointment_Failure_Test()
        {
            // Arrange
            Appointment invalidAppointment = new Appointment(2, 2, DateTime.Now);

            // Act & Assert
            Assert.Throws<AppointmentNotFoundException>(() => appointmentService.UpdateAppointment(invalidAppointment));
        }

        [Test]
        public void UpdateAppointment_Exception_Test()
        {
            // Arrange
            Appointment appointment = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => appointmentService.UpdateAppointment(appointment));
        }
    }
}

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
    public class PatientBLTest
    {
        IRepository<int, Patient> repository;
        IPatientService patientService;
        [SetUp]
        public void Setup()
        {
            repository = new PatientRepository();
            Patient patient = new Patient() { Name = "Phoenix", PatientIllness = "Flu" };
            repository.Add(patient);
            patientService = new PatientService(repository);
        }

        // ADD
        [Test]
        public void AddSuccessTest()
        {
            //Arrange 
            Patient patient = new Patient() {Name = "Andrew", PatientIllness = "Fracture" };
            //Action
            int result = patientService.AddPatient(patient);
            //Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddFailureTest()
        {
            //Arrange 
            Patient patient = new Patient() { Name = "Wendy", PatientIllness = "Asthma" };
            //Action
            int result = patientService.AddPatient(patient);
            //Assert
            Assert.AreNotEqual(1, result);
        }

        [Test]
        public void AddExceptionTest()
        {
            Patient patient = new Patient() { Id = 1, Name = "Wendy", PatientIllness = "Asthma" };
            Assert.Throws<DuplicatePatientException>(() => patientService.AddPatient(patient));
        }

        // GET ALL

        [Test]
        public void GetAllSuccessTest()
        {
            var result = patientService.GetAllPatients();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAllFailureTest()
        {
            var delete = patientService.DeletePatient(1);
            Assert.Throws<PatientNotFoundException>(() => patientService.GetAllPatients());
        }

        [Test]
        public void GetAllExceptionTest()
        {
            var delete = patientService.DeletePatient(1);
            Assert.Throws<PatientNotFoundException>(() => patientService.GetAllPatients());
        }


        // DELETE

        [Test]
        public void DeleteSuccessTest()
        {
            var result = patientService.DeletePatient(1);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void DeleteFailureTest()
        {
            var result = patientService.DeletePatient(1);
            Assert.AreNotEqual(result.Id, 0);
        }

        [Test]
        public void DeleteExceptionTest()
        {
            var result = patientService.DeletePatient(1);
            Assert.Throws<PatientNotFoundException>(() => patientService.DeletePatient(1));
        }

        // UPDATE

        [Test]
        public void UpdateSuccessTest()
        {
            Patient patient = new Patient() { Id = 1, Name = "Phoenix", PatientIllness = "Flu" };
            var result = patientService.UpdatePatient(patient);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void UpdateFailureTest()
        {
            Patient patient = new Patient() { Id = 1, Name = "Phoenix", PatientIllness = "Flu" };
            var result = patientService.UpdatePatient(patient);
            Assert.AreNotEqual(result.Id, 0);
        }

        [Test]
        public void UpdateExceptionTest()
        {
            Patient patient = new Patient() { Id = 101, Name = "Phoenix", PatientIllness = "Flu" };
            Assert.Throws<PatientNotFoundException>(() => patientService.UpdatePatient(patient));
        }

        // GET BY ID

        [Test]
        public void GetByIdSuccessTest()
        {
            var result = patientService.GetPatientById(1);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void GetByIdFailTest()
        {
            Assert.Throws<PatientNotFoundException>(() => patientService.GetPatientById(0));
        }

        [Test]
        public void GetByIdExceptionTest()
        {
            Assert.Throws<PatientNotFoundException>(() => patientService.GetPatientById(0));
        }

    }
}

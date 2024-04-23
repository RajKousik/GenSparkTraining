using DoctorAppointmentBLLibrary;
using DoctorAppointmentBLLibrary.Exceptions;
using DoctorAppointmentDALLibrary;
using DoctorAppointmentModelLibrary;

namespace DoctorAppointmentBLTest
{
    public class DoctorBLTest
    {
        IRepository<int, Doctor> repository;
        IDoctorService doctorService;
        [SetUp]
        public void Setup()
        {
            repository = new DoctorRepository();
            Doctor doctor = new Doctor("Raj", "Cardiologist", "raj@gmail.com");
            repository.Add(doctor);
            doctorService = new DoctorService(repository);
        }

        [Test]
        public void AddSuccessTest()
        {
            //Arrange 
            Doctor doctor = new Doctor() { Name = "JV", Contact = "jv@gmail.com", Specialty = "General" };
            //Action
            int result = doctorService.AddDoctor(doctor);
            //Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddFailureTest()
        {
            //Arrange 
            Doctor doctor = new Doctor() { Name = "Leo", Contact = "leo@gmail.com", Specialty = "Ortho" };
            //Action
            int result = doctorService.AddDoctor(doctor);
            //Assert
            Assert.AreNotEqual(1, result);
        }

        [Test]
        public void AddExceptionTest()
        {
            Doctor doctor = new Doctor() {Id=1, Name = "Leo", Contact = "leo@gmail.com", Specialty = "Ortho" };
            Assert.Throws<DuplicateDoctorException>(() => doctorService.AddDoctor(doctor));
            
        }


        //DELETE

        [Test]
        public void DeleteDoctorSuccessTest()
        {
            // Arrange 
            int doctorIdToDelete = 1; // Assuming this doctor ID exists in the repository
                                      // Action
            var result = doctorService.DeleteDoctor(doctorIdToDelete);
            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void DeleteDoctorFailureTest()
        {
            // Arrange 
            int nonExistentDoctorId = 999; // Assuming this doctor ID does not exist in the repository
                                           // Action
            int invalidDoctorId = -1; // Assuming this is an invalid doctor ID
                                      // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.DeleteDoctor(invalidDoctorId));
        }

        [Test]
        public void DeleteDoctorExceptionTest()
        {
            // Arrange 
            int invalidDoctorId = -1; // Assuming this is an invalid doctor ID
                                      // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.DeleteDoctor(invalidDoctorId));
        }


        //GET-ALL
        [Test]
        public void GetAllDoctorsSuccessTest()
        {
            // Arrange 
            // No need for further arrangement, as all doctors should be retrieved
            // Action
            List<Doctor> doctors = doctorService.GetAllDoctors();
            // Assert
            Assert.IsNotNull(doctors);
      
        }

        [Test]
        public void GetAllDoctorsFailureTest()
        {
            // Arrange 
            // Assuming there are no doctors in the repository
            // Action & Assert
            List<Doctor> doctors = doctorService.GetAllDoctors();
            Assert.IsNotNull(doctors);
        }

        [Test]
        public void GetAllDoctorsExceptionTest()
        {
            // Action & Assert
            repository = new DoctorRepository();
            doctorService = new DoctorService(repository);
            Assert.Throws<DoctorNotFoundException>(() => doctorService.GetAllDoctors());
        }


        //GET-BY-ID
        [Test]
        public void GetDoctorByIdSuccessTest()
        {
            // Arrange 
            int doctorId = 1; // Assuming this doctor ID exists in the repository
                              // Action
            Doctor doctor = doctorService.GetDoctorById(doctorId);
            // Assert
            Assert.AreEqual(doctorId, doctor.Id);
        }

        [Test]
        public void GetDoctorByIdFailureTest()
        {
            // Arrange 
            int nonExistentDoctorId = 999; // Assuming this doctor ID does not exist in the repository
                                           // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.GetDoctorById(nonExistentDoctorId));
        }

        [Test]
        public void GetDoctorByIdExceptionTest()
        {
            // Arrange 
            int invalidDoctorId = -1; // Assuming this is an invalid doctor ID
                                      // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.GetDoctorById(invalidDoctorId));
        }


        //UPDATE

        [Test]
        public void UpdateDoctorSuccessTest()
        {
            // Arrange 
            int doctorIdToUpdate = 1; // Assuming this doctor ID exists in the repository
            Doctor updatedDoctor = new Doctor() { Id = doctorIdToUpdate, Name = "UpdatedName", Contact = "updatedemail@gmail.com", Specialty = "UpdatedSpecialty" };
            // Action
            Doctor result = doctorService.UpdateDoctor(updatedDoctor);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(doctorIdToUpdate, result.Id);
            // You can further assert other properties if needed
        }

        [Test]
        public void UpdateDoctorFailureTest()
        {
            // Arrange 
            int nonExistentDoctorId = 999; // Assuming this doctor ID does not exist in the repository
            Doctor updatedDoctor = new Doctor() { Id = nonExistentDoctorId, Name = "UpdatedName", Contact = "updatedemail@gmail.com", Specialty = "UpdatedSpecialty" };
            // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.UpdateDoctor(updatedDoctor));
        }

        [Test]
        public void UpdateDoctorExceptionTest()
        {
            int nonExistentDoctorId = 999;
            // Arrange 
            Doctor nullDoctor = new Doctor() { Id = nonExistentDoctorId, Name = "UpdatedName", Contact = "updatedemail@gmail.com", Specialty = "UpdatedSpecialty" }; // Assuming this is a null doctor object
                                      // Action & Assert
            Assert.Throws<DoctorNotFoundException>(() => doctorService.UpdateDoctor(nullDoctor));
        }


    }
}
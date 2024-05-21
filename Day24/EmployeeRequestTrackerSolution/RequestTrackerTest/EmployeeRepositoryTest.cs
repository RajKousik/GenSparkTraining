using EmployeeRequestTrackerAPI.Contexts;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Repositories;
using EmployeeRequestTrackerAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerTest
{
    public class EmployeeRepositoryTest
    {
        RequestTrackerContext context;
        IRepository<int, Employee> employeeRepo;
        Employee employee;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                                .UseInMemoryDatabase("dummyDB");
            context = new RequestTrackerContext(optionsBuilder.Options);
            employeeRepo = new EmployeeRepository(context);
            employee = new Employee
            {
                Id = 101,
                Name = "Test1",
                DateOfBirth = new DateTime(2002, 12, 12),
                Phone = "9876543219",
                Role = "Admin",
                Image = ""
            };
            employeeRepo.Add(employee);
        }

        [Test]
        public async Task GetEmployeeByPhoneNoTest() 
        {
            //Arrange
            IEmployeeService employeeService = new EmployeeService(employeeRepo);

            //Action
            var result = await employeeService.GetEmployeeByPhone("9876543219");

            //Assert
            Assert.That(result.Id, Is.EqualTo(employee.Id));
        }
    }
}

using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using ShoppingBLLibrary.BL;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLTest
{
    public class CustomerBLTest
    {
        IRepository<int, Customer> repository;
        ICustomerService customerService;
        [SetUp]
        public void Setup()
        {
            repository = new CustomerRepository();
            Customer customer = new Customer() { Id = 1, Name = "Ash", Age = 21, Phone = "1233456789" };

            customerService = new CustomerBL(repository);
            customerService.AddCustomer(customer);
        }

        // GET BY ID
        [Test]
        public void GetByKeySuccessTest()
        {
            var customer = customerService.GetCustomerById(1);
            Assert.AreEqual(1, customer.Id);
        }

        [Test]
        public void GetByKeyFailureTest()
        {
            Assert.Throws<NoCustomerWithGiveIdException>(() => customerService.GetCustomerById(3));
        }


        // ADD
        [Test]
        public void AddSuccessTest()
        {
            Customer customer = new Customer() { Id = 2, Name = "Ash", Age = 21, Phone = "1233456789" };

            var addedItemId = customerService.AddCustomer(customer);
            var addedItem = customerService.GetCustomerById(addedItemId.Result);

            Assert.IsNotNull(addedItem);
        }

        [Test]
        public void AddFailureTest()
        {
            Customer customer = null;

            Assert.Throws<ArgumentNullException>(() => customerService.AddCustomer(customer));
        }

        [Test]
        public void DeleteSuccessTest()
        {
            Customer customer = new Customer() { Id = 2, Name = "Blue", Age = 21, Phone = "1233456789" };
            var addedItemId = customerService.AddCustomer(customer);

            var deletedItem = customerService.DeleteCustomer(addedItemId.Result);

            Assert.IsNotNull(deletedItem);
        }

        [Test]
        public void DeleteFailureTest()
        {
            Assert.Throws<NoCustomerWithGiveIdException>(() => customerService.DeleteCustomer(1000));
        }

        //GET ALL
        [Test]
        public void GetAllSuccessTest()
        {
            var customers = customerService.GetAllCustomers();

            Assert.IsNotEmpty(customers.Result);
        }

        [Test]
        public void GetAllFailureTest()
        {
            customerService.DeleteCustomer(1);

            Assert.IsEmpty(customerService.GetAllCustomers().Result);
        }


        // UPDATE
        [Test]
        public void UpdateSuccessTest()
        {
            Customer customer = new Customer() { Id = 2, Name = "Blue", Phone = "999999999", Age = 22 };
            var addedId = customerService.AddCustomer(customer);
            customer.Id = addedId.Result;

            var updated = customerService.UpdateCustomer(customer);

            // Assert
            Assert.AreEqual(customer.Id, updated.Id);
        }


        [Test]
        public void UpdateFailureTest()
        {
            Customer customer = null;

            Assert.Throws<NullReferenceException>(() => customerService.UpdateCustomer(customer));
        }
    }
}

using ShoppingApplicationModelLibrary;
using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShoppingDALTest
{
    public class CustomerRepositoryTest
    {
        IRepository<int, Customer> repository;

        [SetUp]
        public void SetUp()
        {
            repository = new CustomerRepository();
            Customer customer = new Customer() { Id = 1, Name = "Ash", Age = 21, Phone = "1234567889" };
            repository.Add(customer);
        }

        // ADD

        [Test]
        public void AddSuccessTest()
        {
            Customer customer = new Customer() { Id = 2, Name = "Neil", Age = 18, Phone = "7071815544" };
            var result = repository.Add(customer);

            Assert.AreEqual(customer.Id, result.Id);
        }

        [Test]
        public void AddFailTest()
        {
            Customer customer = null;
            Assert.Throws<NullReferenceException>(() => repository.Add(customer));
        }

        // GET ALL

        [Test]
        public void GetAllSuccessTest()
        {
            var result = repository.GetAll();
            Assert.AreEqual(1, result.Result.Count);
        }

        [Test]
        public void GetAllFailTest()
        {
            repository.Delete(1);
            var result = repository.GetAll();
            Assert.AreEqual(result.Result.Count, 0);
        }


        // GET BY ID

        [Test]
        public void GetByIdSuccessTest()
        {
            var result = repository.GetByKey(1);

            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void GetByIdFailureTest()
        {
            Assert.Throws<NoCustomerWithGiveIdException>(() => repository.GetByKey(222));
        }


        // DELETE

        [Test]
        public void DeleteSuccessTest()
        {
            var result = repository.Delete(1);

            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void DeleteFailTest()
        {
            Assert.Throws<NoCustomerWithGiveIdException>(() => repository.Delete(200));
        }

        // UPDATE
        [Test]
        public void UpdateSuccessTest()
        {
            Customer customer = new Customer() { Id = 1, Age = 20, Name = "Ash", Phone = "555599269" };
            var result = repository.Update(customer);
            Assert.AreEqual(customer.Id, result.Id);
        }

        [Test]
        public void UpdateFailureTest()
        {
            Customer customer = new Customer() { Id = 1, Age = 20, Name = "Ash", Phone = "555599269" };
            repository.Delete(1);

            Assert.Throws<NoCustomerWithGiveIdException>(() => repository.Update(customer));
        }
    }
}

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
    public class ProductRepositoryTest
    {
        IRepository<int, Product> repository;

        [SetUp]
        public void SetUp()
        {
            repository = new ProductRepository();
            Product product = new Product() { Id = 1, Name = "Pencil", QuantityInHand=5, Price=40.00};
            repository.Add(product);
        }

        [Test]
        public void GetAllSuccessTest()
        {
            var result = repository.GetAll();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAllFailTest()
        {
            repository.Delete(1);
            var result = repository.GetAll();
            Assert.AreEqual(result.Count, 0);
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
            Assert.Throws<NoProductWithGivenIdException>(() => repository.GetByKey(222));
        }

        [Test]
        public void DeleteSuccessTest()
        {
            var result = repository.Delete(1);

            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void DeleteFailTest()
        {
            Assert.Throws<NoProductWithGivenIdException>(() => repository.Delete(200));
        }

        // UPDATE
        [Test]
        public void UpdateSuccessTest()
        {
            Product product = new Product() { Id = 1, Name="New Pencil", QuantityInHand=10, Price=10.00 };
            var result = repository.Update(product);
            Assert.AreEqual(product.Id, result.Id);
        }

        [Test]
        public void UpdateFailureTest()
        {
            Product product = new Product() { Id = 1, Name = "New Pencil", QuantityInHand = 10, Price = 10.00 };

            repository.Delete(1);

            Assert.Throws<NoProductWithGivenIdException>(() => repository.Update(product));
        }

        [Test]
        public void AddSuccessTest()
        {
            Product product = new Product() { Id = 1, Name = "New Pencil", QuantityInHand = 10, Price = 10.00 };

            var result = repository.Add(product);

            Assert.AreEqual(product.Id, result.Id);
        }

        [Test]
        public void AddFailTest()
        {
            Product product = null;
            var result = repository.Add(product);
            Assert.IsNull(result);
        }
    }
}

using ShoppingApplicationModelLibrary.Exceptions;
using ShoppingApplicationModelLibrary;
using ShoppingBLLibrary.Services;
using ShoppingDALLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.BL
{
    public class CustomerBL : ICustomerService
    {
        readonly IRepository<int, Customer> _customerRepository;

        [ExcludeFromCodeCoverage]
        public CustomerBL()
        {
            _customerRepository = new CustomerRepository();
        }

        public CustomerBL(IRepository<int, Customer> repository)
        {

            _customerRepository = repository;
        }
        public int AddCustomer(Customer customer)
        {
            var result = _customerRepository.Add(customer);
            if (result != null)
            {
                return result.Id;
            }
            throw new NoCustomerWithGiveIdException();
        }

        public Customer GetCustomerByName(string name)
        {
            var customer = _customerRepository.GetAll().ToList().Find(e => e.Name == name);
            if (customer == null)
            {
                throw new NoCustomerWithGiveIdException();
            }
            return customer;
        }
        public Customer DeleteCustomer(int id)
        {
            var result = _customerRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }

        public List<Customer> GetAllCustomers()
        {
            var result = _customerRepository.GetAll();
            if (result != null)
            {
                return result.ToList();
            }
            throw new NoCustomerWithGiveIdException();
        }

        public Customer GetCustomerById(int id)
        {
            var result = _customerRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var result = _customerRepository.Update(customer);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }
    }
}

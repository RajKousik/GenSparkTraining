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
        [ExcludeFromCodeCoverage]
        public CustomerBL(IRepository<int, Customer> repository)
        {

            _customerRepository = repository;
        }
        public async Task<int> AddCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var result = await _customerRepository.Add(customer);
            if (result != null)
            {
                return result.Id;
            }
            throw new NoCustomerWithGiveIdException();
        }
        [ExcludeFromCodeCoverage]
        public async Task<Customer> GetCustomerByName(string name)
        {
            var customer = await _customerRepository.GetAll();
            var customerToBeReturned = customer.ToList().Find(e => e.Name == name);
            if (customerToBeReturned == null)
            {
                throw new NoCustomerWithGiveIdException();
            }
            return customerToBeReturned;
        }
        public async Task<Customer> DeleteCustomer(int id)
        {
            var result = await _customerRepository.Delete(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var result = await _customerRepository.GetAll();
            if (result != null)
            {
                return result.ToList();
            }
            throw new NoCustomerWithGiveIdException();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var result = await _customerRepository.GetByKey(id);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var result = await _customerRepository.Update(customer);
            if (result != null)
            {
                return result;
            }
            throw new NoCustomerWithGiveIdException();
        }
    }
}

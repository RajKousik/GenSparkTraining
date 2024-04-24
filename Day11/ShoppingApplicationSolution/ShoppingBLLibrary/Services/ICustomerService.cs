using ShoppingApplicationModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary.Services
{
    public interface ICustomerService
    {
        int AddCustomer(Customer customer);
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
        Customer UpdateCustomer(Customer customer);
        Customer DeleteCustomer(int id);
    }
}

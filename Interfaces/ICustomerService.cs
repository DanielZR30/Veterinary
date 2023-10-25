using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    internal interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<Customer> GetCustomerById(Guid customerId);

        Task<IEnumerable<Customer>> GetCustomers();

        Task<Customer> UpdateCustomer(Customer customer);

        Task<Customer> DeleteCustomer(Customer customer);
    }
}

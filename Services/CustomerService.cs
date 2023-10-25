using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veterinary.Interfaces;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly VeterinaryEntities _context;

        public CustomerService(VeterinaryEntities context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            Customer c = await _context.Customer.FirstOrDefaultAsync(cus => cus.CustomerPhone == customer.CustomerPhone);
            if (c == null)
            {
                c = _context.Customer.Add(customer);
                await _context.SaveChangesAsync();
            }

            return c;


        }

        public async Task<Customer> DeleteCustomer(Customer customer)
        {
            Customer c = _context.Customer.FirstOrDefault(cus => cus.IDCustomer == customer.IDCustomer);
            try
            {
                if (c != null)
                {
                    _context.Customer.Remove(c);
                    await _context.SaveChangesAsync();
                }
                return c;
            }
            catch (Exception ex)
            {
                return c;
            }
        }

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.IDCustomer.Equals(customerId));
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _context.Customer.ToListAsync();
            return customers;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Customer c = _context.Customer.FirstOrDefault(cus => cus.IDCustomer == customer.IDCustomer);
            try
            {
                c.CustomerName = customer.CustomerName;
                c.CustomerLastname = customer.CustomerLastname;
                c.CustomerPhone = customer.CustomerPhone;
                await _context.SaveChangesAsync();
                return c;
            }
            catch (Exception ex)
            {
                return c;
            }
        }
    }
}
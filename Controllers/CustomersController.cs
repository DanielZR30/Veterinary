using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.Services;
using Veterinary.ViewModels;

namespace Veterinary.Controllers
{
    [EnableCors(origins: "http://localhost:50804", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly ICustomerService _customerService = new CustomerService(_context);

        #region Create

        [HttpPost]
        [Route("api/customers/create")]
        public async Task<object> CreateCustomer([FromBody] CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer customer = new Customer
                    {
                        IDCustomer = Guid.NewGuid(),
                        CustomerName = customerViewModel.CustomerName,
                        CustomerLastname = customerViewModel.CustomerLastname,
                        CustomerPhone = customerViewModel.CustomerPhone,
                        Pet= new List<Pet>(),
                        Invoice= new List<Invoice>()
                    };
                    await _customerService.CreateCustomer(customer);
                    return Ok(customer);
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError("Duplicated", "Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("api/customers")]
        public async Task<object> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = await _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/customers/{customerId}")]
        public async Task<object> GetCustomerById(Guid customerId)
        {
            try
            {
                Customer customer = await _customerService.GetCustomerById(customerId);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("api/customers/{customerId}")]
        public async Task<object> UpdateProduct(Guid customerId, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer c = await _customerService.UpdateCustomer(customer);
                    return Ok(c);
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError("Duplicated", "Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("api/customers/{customerId}")]
        public async Task<object> DeleteProduct(Guid customerId, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Customer c = await _customerService.DeleteCustomer(customer);
                    return Ok(c);
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError("Duplicated", "Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return BadRequest(ModelState.ToString());
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]string email) {
            return Ok(_customerRepository.GetCustomers(email));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Managment.Core.Domain.Customer customer) {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                Managment.Core.Domain.Customer customer1 = new Managment.Core.Domain.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    DateOfBirth = customer.DateOfBirth,
                    City = customer.City,
                    HouseNumber = customer.HouseNumber,
                    PostCode = customer.PostCode,
                    Street = customer.Street,
                    TelephoneNumber = customer.TelephoneNumber

                };

                _customerRepository.AddCustomer(customer1);
                result = Ok(customer1);
            }
            else {
                result = BadRequest(ModelState);
            }
            return result;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Managment.Core.Domain.Customer prod)
        {
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}

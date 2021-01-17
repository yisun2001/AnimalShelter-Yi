using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly AnimalDbContext _context;

        public CustomerRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Customer> GetCustomers(string email = null){
            if (!string.IsNullOrEmpty(email))
            {
                return _context.Customers.Where(customer => customer.Email == email);
            }
            else {
                return _context.Customers;
            }
        }
        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers.SingleOrDefault(Customer => Customer.Email == email);
        }

        public IQueryable<Customer> GetAll()
        {
            return _context.Customers.Include(g => g.Interests).Include(g => g.AnimalForAdoptions);
        }
    }
}


using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
        IQueryable<Customer> GetAll();

        IEnumerable<Customer> GetCustomers(string email = null);
        Customer GetCustomerByEmail(string email);
    }
}

using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IInterestRepository
    {
        IEnumerable<Interest> GetAll();
        IEnumerable<Interest> GetAllInterests(string customerId = null);
        Task AddInterest(Interest interest);

        Interest GetById(int id);

        Interest GetByCustomerId(int id);

        Interest GetByAnimalId(int id);

        Task DeleteInterest(Interest interest);
    }
}

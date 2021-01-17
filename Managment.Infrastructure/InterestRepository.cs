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
    public class InterestRepository : IInterestRepository
    {
        private readonly AnimalDbContext _context;

        public InterestRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddInterest(Interest interest)
        {
            await _context.Interests.AddAsync(interest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInterest(Interest interest)
        {
            if (interest == null) throw new ArgumentNullException(nameof(interest));

            _context.Interests.Remove(interest);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Interest> GetAll()
        {
            return _context.Interests.Include(g => g.Animal)
                .Include(g => g.Customer);
        }

        public IEnumerable<Interest> GetAllInterests(string customerId = null)
        {
            if (!string.IsNullOrEmpty(customerId))
            {
                return _context.Interests.Where(interest => interest.CustomerId == int.Parse(customerId));

            }
            else {
                return _context.Interests;
            }
                
           
            
        }

        public Interest GetByAnimalId(int id)
        {
            return _context.Interests.SingleOrDefault(interest => interest.AnimalId == id);
        }

        public Interest GetByCustomerId(int id)
        {
            return _context.Interests.SingleOrDefault(interest => interest.CustomerId == id);
        }

        public Interest GetById(int id)
        {
            return _context.Interests.SingleOrDefault(interest => interest.Id == id);
        }
    }
}

using Managment.Core.Domain;
using Managment.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Infrastructure
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly AnimalDbContext _context;

        public VolunteerRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddVolunteer(Volunteer volunteer)
        {
            await _context.Volunteers.AddAsync(volunteer);
            await _context.SaveChangesAsync();
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            return _context.Volunteers.SingleOrDefault(Volunteer => Volunteer.Email == email);
        }
    }
}

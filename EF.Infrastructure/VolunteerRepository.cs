using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Infrastructure
{
    public class VolunteerRepository : IVolunteerRepository
        
    {
        private readonly AnimalShelterDbContext _context;

      

        public VolunteerRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }

        public Volunteer CreateVolunteer(Volunteer volunteer)
        {
            _context.Volunteers.Add(volunteer);
            _context.SaveChanges();
            return volunteer;
        }
        public Volunteer GetVolunteer(int Id) {

            var vol = _context.Volunteers.Find(Id);
            return vol;

        }

        public IEnumerable<Volunteer> GetAllVolunteers()
        {
            return _context.Volunteers;
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            var vol = _context.Volunteers.Where(a=>a.Email==email);
            return vol.FirstOrDefault();
        }


    }
}

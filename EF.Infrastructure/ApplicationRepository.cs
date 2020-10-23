using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AnimalShelterDbContext _context;

        public ApplicationRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Application> GetAllApplications()
        {
            return _context.Applications;
        }
    }
}

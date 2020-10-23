using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly AnimalShelterDbContext _context;

        public TreatmentRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }

        public Treatment CreateTreatment(Treatment treatment)
        {

            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            return treatment;
        }
    }
}

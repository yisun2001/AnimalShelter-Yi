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
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly AnimalDbContext _context;

        public TreatmentRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddTreatment(Treatment newtreament)
        {
            await _context.Treatments.AddAsync(newtreament);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Treatment> GetAllTreatments()
        {
            return _context.Treatments;
        }

        public Treatment GetById(int id)
        {
            return _context.Treatments.SingleOrDefault(Treatment => Treatment.Id == id);
        }

        public IEnumerable<Treatment> GetByAnimalId(int id)
        {
            return _context.Treatments?.Where(Treatment => Treatment.AnimalId == id);
        }

        public async Task DeleteTreatment(Treatment treatment)
        {
            if (treatment == null) throw new ArgumentNullException(nameof(treatment));

            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTreatment(Treatment treatment)
        {
            var treatmentToUpdate = _context.Treatments.Attach(treatment);
            treatmentToUpdate.State = EntityState.Modified;

            //_context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }
    }
}

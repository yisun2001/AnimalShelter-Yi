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

        public Treatment DeleteTreatment(int Id)
        {
            Treatment treatment = GetTreatment(Id);
            _context.Remove(GetTreatment(Id));
            _context.SaveChanges();

            return treatment;
        }

        public Treatment CreateTreatment(Treatment treatment)
        {

            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            return treatment;
        }

        public Treatment UpdateTreatment(Treatment treatment)
        {
            // var an = _context.Animals.Attach(animal);
            // an.State = EntityState.Modified;
            var tr = GetTreatment(treatment.Id);
            _context.Entry(tr).CurrentValues.SetValues(treatment);

            _context.SaveChanges();
            return treatment;

        }

        public Treatment GetTreatment(int Id)
        {
            var treatment = _context.Treatments.Find(Id);
            return treatment;
        }
    }
}

using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly AnimalShelterDbContext _context;
        private readonly IAnimalRepository _animalRepository;

        public TreatmentRepository(AnimalShelterDbContext context, IAnimalRepository animalRepository )
        {
            this._context = context;
            this._animalRepository = animalRepository;
        }

        public Treatment CreateTreatment(Treatment treatment)
        {
            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            return treatment;
        }


        /*    nog bewerken zodat het aan een animal gevoegd wordt*/
        public Treatment CreateTreatment(Treatment treatment, int Id)
        {
            _context.Treatments.Add(treatment);
            _context.SaveChanges();
            return treatment;
        }

        public Treatment DeleteTreatment(int Id)
        {
            Treatment treatment = GetTreatment(Id);
            _context.Remove(GetTreatment(Id));
            _context.SaveChanges();

            return treatment;
        }

        public IEnumerable<Treatment> GetAllTreatments()
        {
            return _context.Treatments;
        }

        public Treatment GetTreatment(int Id)
        {
            return _context.Treatments.Find(Id);
        }


       /* nog bewerken zodat het bij een animal geupdate wordt*/
        public Treatment UpdateTreatment(Treatment treatment, int Id)
        {
            var treat = _context.Treatments.Attach(treatment);
            treat.State = EntityState.Modified;
            _context.SaveChanges();
            return treatment;

        }

            public Treatment UpdateTreatment(Treatment treatment)
            {
                var treat = _context.Treatments.Attach(treatment);
                treat.State = EntityState.Modified;
                _context.SaveChanges();
                return treatment;



            }
        }
}

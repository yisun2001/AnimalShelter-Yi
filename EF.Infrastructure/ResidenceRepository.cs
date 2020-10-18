using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace EF.Infrastructure
{
    public class ResidenceRepository : IResidenceRepository
    {
        private readonly AnimalShelterDbContext _context;

        public ResidenceRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }


        public IEnumerable<Residence> GetAllResidences()
        {
            return _context.Residences.Include(p => p.Animals);
        }

        public Residence GetResidence(int Id)
        {
            return _context.Residences.Find(Id);
        }

        public Residence DeleteResidence(int Id)
        {
            Residence residence = GetResidence(Id);
            _context.Remove(GetResidence(Id));
            _context.SaveChanges();

            return residence;
        }

        public Residence CreateResidence(Residence residence)
        {
            _context.Residences.Add(residence);
            _context.SaveChanges();
            return residence;
        }

        public Residence UpdateResidence(Residence residence)
        {
            var resi = _context.Residences.Attach(residence);
            resi.State = EntityState.Modified;
            _context.SaveChanges();
            return residence;
        }

       
    }
}

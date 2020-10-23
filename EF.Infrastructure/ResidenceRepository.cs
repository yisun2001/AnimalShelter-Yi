using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Residences;
        }
        public ICollection<Animal> GetResidenceAnimals(int Id)
        {
            var list = _context.Animals.Where(a => a.ResidenceId == Id).ToList();
            return list;
        }
        public Residence GetResidence(int Id)
        {
            var residence = _context.Residences.Find(Id);
            if (residence == null)
            {
                return residence;
            }
            else 
            {
                residence.Animals = GetResidenceAnimals(Id);
                return residence;
            }
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

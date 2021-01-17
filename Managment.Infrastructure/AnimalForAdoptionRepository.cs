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
    public class AnimalForAdoptionRepository : IAnimalForAdoptionRepository
    {
        private readonly AnimalDbContext _context;

        public AnimalForAdoptionRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddAnimal(AnimalForAdoption animal)
        {
            await _context.AnimalForAdoptions.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public IQueryable<AnimalForAdoption> GetAll()
        {
            return _context.AnimalForAdoptions.Include(g => g.Customer);
        }

        public IEnumerable<AnimalForAdoption> GetAllAnimals()
        {
            return _context.AnimalForAdoptions;
        }

        public AnimalForAdoption GetByCustomerId(int id)
        {
            return _context.AnimalForAdoptions.SingleOrDefault(animal => animal.CustomerId == id);
        }

        public AnimalForAdoption GetById(int id)
        {
            return _context.AnimalForAdoptions.SingleOrDefault(animal => animal.Id == id);
        }
    }
}

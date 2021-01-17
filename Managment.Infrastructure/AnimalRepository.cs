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
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _context;

        public AnimalRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddAnimal(Animal newanimal)
        {
            await _context.Animals.AddAsync(newanimal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimal(Animal animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Animal> GetAll()
        {
            return _context.Animals.Include(g => g.Notations).
                Include(g => g.Residence).
                Include(g => g.Treatments);
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _context.Animals;
        }

        public IEnumerable<Animal> GetAllAnimalsAdoptable(string type = null, string gender = null, string kidFriendly = null, string adopable = null)
        {
            if (!string.IsNullOrEmpty(adopable)) {
                return _context.Animals;
            }else
            if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(gender) && string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true);
            }else
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.Gender.Equals((Gender)Enum.Parse(typeof(Gender), gender)) &&
                                              animal.Type.Equals((AnimalType)Enum.Parse(typeof(AnimalType), type)) &&
                                              animal.KidFriendly.Equals((KidFriendly)Enum.Parse(typeof(KidFriendly), kidFriendly)));
            }
            else if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.Gender.Equals((Gender)Enum.Parse(typeof(Gender), gender)) &&
                                              animal.KidFriendly.Equals((KidFriendly)Enum.Parse(typeof(KidFriendly), kidFriendly)));
            }
            else if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.Type.Equals((AnimalType)Enum.Parse(typeof(AnimalType), type)) &&
                                              animal.KidFriendly.Equals((KidFriendly)Enum.Parse(typeof(KidFriendly), kidFriendly)));
            }
            else if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(gender) && string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.Gender.Equals((Gender)Enum.Parse(typeof(Gender), gender)) &&
                                              animal.Type.Equals((AnimalType)Enum.Parse(typeof(AnimalType), type)));
            }
            else if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.KidFriendly.Equals((KidFriendly)Enum.Parse(typeof(KidFriendly), kidFriendly)));
            }
            else if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(gender) && string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&

                                              animal.Type.Equals((AnimalType)Enum.Parse(typeof(AnimalType), type)));
            }
            else if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(gender) && string.IsNullOrEmpty(kidFriendly))
            {
                return _context.Animals.Where(animal => animal.Adoptable == true &&
                                              animal.Gender.Equals((Gender)Enum.Parse(typeof(Gender), gender)));
            }
            else {
                return _context.Animals.Where(animal => animal.Adoptable == true
                                                  );
            }
           
        }

        public IEnumerable<Animal> GetAnimalsByResidence(int Id)
        {
            return _context.Animals.Where(animal => animal.ResidenceId == Id);
        }

        public Animal GetById(int id)
        {
            return _context.Animals.SingleOrDefault(Animal => Animal.Id == id);
        }

        public async Task UpdateAnimal(Animal animal)
        {
            var animalToUpdate = _context.Animals.Attach(animal);
            animalToUpdate.State = EntityState.Modified;

            //_context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }
    }
}

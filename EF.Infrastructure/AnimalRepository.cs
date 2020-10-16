using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.Infrastructure
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalShelterDbContext _context;

        public AnimalRepository(AnimalShelterDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _context.Animals;
        }

        public Animal GetAnimal(int Id)
        {
            return _context.Animals.Find(Id);
        }

        public Animal CreateAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
            return animal;
        }

        public Animal DeleteAnimal(int Id)
        {
            Animal animal = GetAnimal(Id);
            _context.Remove(GetAnimal(Id));
            _context.SaveChanges();

            return animal;
        }


        public Animal UpdateAnimal(Animal animal)
        {
            var anim = _context.Animals.Attach(animal);
            anim.State = EntityState.Modified;
            _context.SaveChanges();
            return animal;
        }
    }
}

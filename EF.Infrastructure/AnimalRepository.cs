using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Animal CreateAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
            return animal;
        }

        public Animal DeleteAnimal(int Id)
        {
            Animal animal = GetAnimal(Id);
            _context.Remove(animal);
            _context.SaveChanges();

            return animal;
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _context.Animals;
        }

        public Animal GetAnimal(int Id)
        {
            var animal = _context.Animals.Find(Id);

            if (animal == null) {
                return animal;
            }
            animal.Comments = GetAnimalComments(Id);
            animal.Treatments = GetAnimalTreatment(Id);
            return animal;
        }

        public ICollection<Comment> GetAnimalComments(int Id)
        {
            //var blogs = context.Blogs
            //.Include(blog => blog.Posts)
            //    .ThenInclude(post => post.Author)
            //    .ToList();

            var list = _context.Comments.Where(a => a.AnimalId == Id).ToList();
            return list;
        }

        public ICollection<Treatment> GetAnimalTreatment(int Id)
        {
            var list = _context.Treatments.Where(a => a.AnimalId == Id).ToList();
            return list;
        }

        public Animal UpdateAnimal(Animal animal)
        {
            // var an = _context.Animals.Attach(animal);
            // an.State = EntityState.Modified;
            var an = GetAnimal(animal.Id);
            _context.Entry(an).CurrentValues.SetValues(animal);

            _context.SaveChanges();
            return animal;

        }

        public int count() {

            int count = GetAllAnimals().Count();
            return count;
        }
    }
}

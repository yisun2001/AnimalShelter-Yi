using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IAnimalRepository
    {
        IQueryable<Animal> GetAll();

        IEnumerable<Animal> GetAllAnimals();

        IEnumerable<Animal> GetAllAnimalsAdoptable(string type = null, string gender = null, string kidFriendly = null, string adoptable = null);

        IEnumerable<Animal> GetAnimalsByResidence(int Id);

        //IEnumerable<Animal> GetAllDogs();

        //IEnumerable<Animal> GetAllCats();

        Task AddAnimal(Animal newanimal);

        Task DeleteAnimal(Animal animal);

        Task UpdateAnimal(Animal animal);

        Animal GetById(int id);
    }
}

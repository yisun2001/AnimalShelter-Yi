using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IAnimalForAdoptionRepository
    {
        IEnumerable<AnimalForAdoption> GetAllAnimals();
        IQueryable<AnimalForAdoption> GetAll();
        Task AddAnimal(AnimalForAdoption animal);

        AnimalForAdoption GetById(int id);
        AnimalForAdoption GetByCustomerId(int id);
    }
}

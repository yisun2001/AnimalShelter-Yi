using Core.Domain;
using System;
using System.Collections.Generic;

namespace Core.DomainServices
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAllAnimals();

        Animal GetAnimal(int Id);

        Animal DeleteAnimal(int Id);

        Animal CreateAnimal(Animal animal);

        Animal UpdateAnimal(Animal animal);


    }
}

using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface IAnimalServices
    {
        Task AddAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);

    }
}

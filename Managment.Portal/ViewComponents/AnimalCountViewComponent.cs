using Managment.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.ViewComponents
{
    public class AnimalCountViewComponent : ViewComponent
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalCountViewComponent(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public string Invoke() {
            var result = _animalRepository.GetAllAnimals().Count();

            return result switch
            {
                0=> "- Er zijn geen dieren in het asiel",
                1=> "-  Er is 1 dier in het asiel",
                _=> $"- Er zijn {result} dieren in het asiel"
            };
        }
    }
}

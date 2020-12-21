using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace UI.ASMApp.ViewComponents
{
    public class AnimalCountViewComponent : ViewComponent 
    {

        private readonly IAnimalRepository _animalRepository;

        public AnimalCountViewComponent(IAnimalRepository animalRepository) {

            _animalRepository = animalRepository;

        }


   

        public IViewComponentResult Invoke()
        {
            ViewBag.result = _animalRepository.count();

            return View("Default", ViewBag.result);
        }
    }
}

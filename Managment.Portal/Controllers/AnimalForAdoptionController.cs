using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.DomainServices;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Managment.Portal.Controllers
{
    public class AnimalForAdoptionController : Controller
    {
        private readonly IAnimalForAdoptionRepository _animalForAdoptionRepository;

        public AnimalForAdoptionController(IAnimalForAdoptionRepository animalForAdoptionRepository)
        {
            _animalForAdoptionRepository = animalForAdoptionRepository;
        }
        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult Overview()
        {
            return View(_animalForAdoptionRepository.GetAll().OrderByDescending(g => g.AddedOn).ToViewModel());
        }
    }
}

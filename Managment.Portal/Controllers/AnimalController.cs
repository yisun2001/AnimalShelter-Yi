using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Managment.Infrastructure;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Managment.Portal.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IAnimalServices _animalServices;

        public AnimalController(
            ILogger<HomeController> logger,
            IAnimalRepository animalRepository,
            INoteRepository noteRepository,
            IResidenceRepository residenceRepository,
            ITreatmentRepository treatmentRepository,
            IAnimalServices animalServices
            )
        {
            _logger = logger;
            _animalRepository = animalRepository;
            _noteRepository = noteRepository;
            _residenceRepository = residenceRepository;
            _treatmentRepository = treatmentRepository;
            _animalServices = animalServices;
        }

        [Authorize (Policy = "VolunteerOnly")]
        public IActionResult Overview()
        {
            return View(_animalRepository.GetAll().ToViewModel());
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpGet]
        public IActionResult AddAnimal()
        {
            var model = new AddAnimalViewModel();

            PrefillSelectOptions();

            return View(model);
        }

        private void PrefillSelectOptions()
        {
            var residences = _residenceRepository.GetAllResidences().Prepend(new Residence() { Id = -1, Name = "Selecteer een verblijf" });
            ViewBag.Residences = new SelectList(residences, "Id", "Name");


            var types = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>();
            ViewBag.Types = new SelectList(types, "Type" );

            var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            ViewBag.Genders = new SelectList(genders, "Gender");

            var kidFriendly = Enum.GetValues(typeof(KidFriendly));
            ViewBag.KidFriendly = new SelectList(kidFriendly, "KidFriendly");
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalViewModel newanimal)
        {
            
            
                try
                {

                    Animal animalToCreate = new Animal
                    {
                        Adoptable = newanimal.Adoptable,
                        Name = newanimal.Name,
                        Race = newanimal.Race,
                        ReasonAdoptable = newanimal.ReasonAdoptable,
                        KidFriendly = newanimal.KidFriendly,
                        DateOfBirth = newanimal.DateOfBirth,
                        DateOfArrival = (DateTime)newanimal.DateOfArrival,
                        Description = newanimal.Description,
                        Gender = newanimal.Gender,
                        ResidenceId = newanimal.ResidenceId,
                        Type = newanimal.Type,
                        Sterilised = newanimal.Sterilised,
                        EstimatedAge = newanimal.EstimatedAge
                    };

                    if (newanimal.Image != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await newanimal.Image.CopyToAsync(stream);
                            animalToCreate.Image = stream.ToArray();
                        }
                    }
                if (ModelState.IsValid)
                {
                    await _animalServices.AddAnimal(animalToCreate);
                    return RedirectToAction("Overview");
                }
                }

                catch (InvalidOperationException e)
                {
                    //return View(e.Message);
                    
                    ModelState.AddModelError("", e.Message);
                }
            
            
            PrefillSelectOptions();
           
            return View();     
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdateAnimal(int Id)
        {
            var animal = _animalRepository.GetById(Id);
            

            var model = new UpdateAnimalViewModel() {
                Id = animal.Id,
                Adoptable = animal.Adoptable,
                Name = animal.Name,
                Race = animal.Race,
                ReasonAdoptable = animal.ReasonAdoptable,
                KidFriendly = animal.KidFriendly,
                DateOfBirth = animal.DateOfBirth,
                DateOfArrival = animal.DateOfArrival,
                DateOfAdoption = animal.DateOfAdoption,
                DateOfPassing = animal.DateOfPassing,
                EstimatedAge = animal.EstimatedAge,
                AdoptedBy = animal.AdoptedBy,
                Description = animal.Description,
                Gender = animal.Gender,
                ResidenceId = animal.ResidenceId,
                Type = animal.Type,
                Sterilised = animal.Sterilised

            };

            if (animal.Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    //model.Image = await animal.Image.ReadAsync(stream);
                }
            }


            PrefillSelectOptionsUpdate();
       
            return View(model);
        }

        private void PrefillSelectOptionsUpdate()
        {
            var residences = _residenceRepository.GetAllResidences().Prepend(new Residence() { Id = -1, Name = "Selecteer een verblijf" });
            ViewBag.Residences = new SelectList(residences, "Id", "Name");


            var types = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>();
            ViewBag.Types = new SelectList(types, "Type");

            var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            ViewBag.Genders = new SelectList(genders, "Gender");

            var kidFriendly = Enum.GetValues(typeof(KidFriendly));
            ViewBag.KidFriendly = new SelectList(kidFriendly, "KidFriendly");


        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public async Task<IActionResult> UpdateAnimal(UpdateAnimalViewModel animal)
        {
                try {
                var animalToUpdate = _animalRepository.GetById(animal.Id);

                if (ModelState.IsValid)
                {
                    animalToUpdate.Adoptable = animal.Adoptable;
                    animalToUpdate.Name = animal.Name;
                    animalToUpdate.Race = animal.Race;
                    animalToUpdate.ReasonAdoptable = animal.ReasonAdoptable;
                    animalToUpdate.KidFriendly = animal.KidFriendly;
                    animalToUpdate.DateOfBirth = animal.DateOfBirth;
                    animalToUpdate.DateOfArrival = animal.DateOfArrival;
                    animalToUpdate.Description = animal.Description;
                    animalToUpdate.Gender = animal.Gender;
                    animalToUpdate.ResidenceId = animal.ResidenceId;
                    animalToUpdate.Type = animal.Type;
                    animalToUpdate.Sterilised = animal.Sterilised;
                    animalToUpdate.DateOfPassing = animal.DateOfPassing;
                    animalToUpdate.DateOfAdoption = animal.DateOfAdoption;
                    animalToUpdate.AdoptedBy = animal.AdoptedBy;
                    animalToUpdate.EstimatedAge = animal.EstimatedAge;

                    if (animal.Image != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await animal.Image.CopyToAsync(stream);
                            animalToUpdate.Image = stream.ToArray();
                        }
                    }
                    await _animalServices.UpdateAnimal(animalToUpdate);
                    return RedirectToAction("Overview");
                }
                } catch (InvalidOperationException e) {
                         ModelState.AddModelError("", e.Message);
                }
                

            PrefillSelectOptions();
            return View(animal);
        }
        [Authorize(Policy = "VolunteerOnly")]
        public async Task<IActionResult> DeleteAnimal(int Id)
        {
            var animal = _animalRepository.GetById(Id);
            await _animalRepository.DeleteAnimal(animal);
            return RedirectToAction("Overview");
        }
 

    }
}

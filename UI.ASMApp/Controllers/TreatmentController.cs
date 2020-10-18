using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using UI.ASMApp.Models;
using Microsoft.AspNetCore.Hosting;




namespace UI.ASMApp.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly ILogger<TreatmentController> _logger;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IAnimalRepository _animalRepository;


        public TreatmentController(ILogger<TreatmentController> logger, ITreatmentRepository treatmentRepository, IAnimalRepository animalRepository)
        {
            _logger = logger;
            this._treatmentRepository = treatmentRepository;
            this._animalRepository = animalRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Treatment> lists = _treatmentRepository.GetAllTreatments().ToList();
            var model = lists;
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _treatmentRepository.GetTreatment(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
/*
        [HttpPost]
        public IActionResult Create(Treatment model, Animal animal)
        {
            if (ModelState.IsValid) { 
          
                Treatment treatment = new Treatment
                {

                    TypeOfTreatment = model.TypeOfTreatment,
                    Description = model.Description,
                    Costs = model.Costs,
                    AgeRequirement = model.AgeRequirement,
                    DateOfTime = model.DateOfTime,
                    Animal = model.Animal,
                   
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table

                };
                animal.Treatments.Add(treatment);

                return RedirectToAction("details", new { id = animal.Id });
            }

            return View();
        }*/

        [HttpPost]
        public IActionResult Create(CreateTreatmentViewModel model, int Id)
        {

            var animal = _animalRepository.GetAnimal(Id);

            if (model.TypeOfTreatment == TypeOfTreatment.Castration || model.TypeOfTreatment == TypeOfTreatment.Sterilization) 
            {

                if (animal.Age < 0.5)
                {

                    ModelState.AddModelError(nameof(model.TypeOfTreatment), "De behandeling mag pas na de leeftijd van 6 maanden geëxcuteerd worden.");
                }
            }
            

            if (ModelState.IsValid)
            {

                if (model.DateOfTime < animal.DateOfArrival) {
                    ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is vóór het moment van arriveren.");
                }
                
                if (model.DateOfTime > animal.DateOfDeath) {
                    ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is na het overlijden van het dier.");

                    switch (model.TypeOfTreatment)
                    {
                        case TypeOfTreatment.Vaccination:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een omschrijving verplicht.");
                            break;

                        case TypeOfTreatment.Operation:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een omschrijving van de type operatie benodigd.");
                            break;

                        case TypeOfTreatment.Chipping:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een chipcode (GUID) verplicht.");
                            break;

                        case TypeOfTreatment.Euthanasia:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een beschreven reden nodig.");
                            break;
                    }
                }


                Treatment newTreatment = new Treatment
                {

                    TypeOfTreatment = model.TypeOfTreatment,
                    Description = model.Description,
                    TreatmentExecutedby = model.TreatmentExecutedby,
                    Costs = model.Costs,
                    AgeRequirement = model.AgeRequirement,
                    DateOfTime = model.DateOfTime,


                };
                try { 
                _treatmentRepository.CreateTreatment(newTreatment, Id);
                return RedirectToAction("details", new { id = newTreatment.Id });
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }




[HttpPost]
        public IActionResult Delete(int id)
        {
            
            _treatmentRepository.DeleteTreatment(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Treatment treatment = _treatmentRepository.GetTreatment(id);
            return View(treatment);
        }

        [HttpPost]
        public IActionResult Edit(Treatment treatment, int Id)
        {
            if (ModelState.IsValid)
            {
                _treatmentRepository.UpdateTreatment(treatment, Id);
                return RedirectToAction("index");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

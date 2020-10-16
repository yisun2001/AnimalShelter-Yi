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
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private string uploadsFolder;
        private readonly IHostingEnvironment hostingEnvironment;

        public TreatmentController(ILogger<HomeController> logger, IAnimalRepository animalRepository, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            this._animalRepository = animalRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Animal> lists = _animalRepository.GetAllAnimals().ToList();
            var model = lists;
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _animalRepository.GetAnimal(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

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
        }




[HttpPost]
        public IActionResult Delete(int id)
        {
            
            _animalRepository.DeleteAnimal(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Animal animal = _animalRepository.GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.UpdateAnimal(animal);
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

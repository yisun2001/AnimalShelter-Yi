using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.ASMApp.Models;

namespace UI.ASMApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;

        public HomeController(ILogger<HomeController> logger, IAnimalRepository animalRepository)
        {
            _logger = logger;
            this._animalRepository = animalRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _animalRepository.GetAllAnimals().ToList();
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
        public IActionResult Create(Animal model)
        {
            if (ModelState.IsValid)
            {
                Animal newAnimal = model;
                _animalRepository.CreateAnimal(newAnimal);
                return RedirectToAction("details", new { id = newAnimal.Id });
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

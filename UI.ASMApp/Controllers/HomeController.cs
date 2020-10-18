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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly IResidenceRepository _residenceRepository;

        private string uploadsFolder;
        private readonly IHostingEnvironment hostingEnvironment;
        public static bool HasValue;

        public HomeController(ILogger<HomeController> logger, IAnimalRepository animalRepository, IHostingEnvironment hostingEnvironment, IResidenceRepository residenceRepository)
        {
            _logger = logger;
            this._animalRepository = animalRepository;
            this._residenceRepository = residenceRepository;
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
        public IActionResult Create(CreateAnimalViewModel model)
        {


            if ((model?.DateOfBirth != null && model?.EstimatedAge != null) || (model?.DateOfBirth == null && model?.EstimatedAge == null)) {

                ModelState.AddModelError(nameof(model.EstimatedAge), "Geschatte leeftijd óf leeftijd moet ingevuld worden.");
            }

                if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Image != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images/AnimalImages");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Animal newAnimal = new Animal
                {

                    Name = model.Name,
                    DateOfBirth = model.DateOfBirth,
                    Age = model.Age,
                    EstimatedAge = model.EstimatedAge,
                    Description = model.Description,
                    TypeOfAnimal = model.TypeOfAnimal,
                    Breed = model.Breed,
                    Gender = model.Gender, 
                    ImagePath = uniqueFileName,
                    DateOfArrival = model.DateOfArrival,
                    DateOfAdoption = model.DateOfAdoption,
                    DateOfDeath = model.DateOfDeath,
                    IsNeutered = model.IsNeutered,
                    CompatibleWithKids = model.CompatibleWithKids,
                    ReasonOfDistancing = model.ReasonOfDistancing,
                    Treatments = model.Treatments,
                    Comments = model.Comments,
                    AdoptedBy = model.AdoptedBy,
                    ClientNumber = model.ClientNumber,
                    Residence = model.Residence,
                    ResidenceId = model.ResidenceId,
                    Volunteer = model.Volunteer,
                    VolunteerId = model.VolunteerId,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table

                };
               

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

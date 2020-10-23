using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.ASMApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;


namespace UI.ASMApp.Controllers
{
    [Authorize(Policy = "Volunteer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IVolunteerRepository volunteerRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        public static bool HasValue;

        public HomeController(ILogger<HomeController> logger, IAnimalRepository animalRepository, IHostingEnvironment hostingEnvironment, ICommentRepository commentRepository, ITreatmentRepository treatmentRepository, IResidenceRepository residenceRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IVolunteerRepository volunteerRepository)
        {
            _logger = logger;
            this._animalRepository = animalRepository;
            this._commentRepository = commentRepository;
            this._treatmentRepository = treatmentRepository;
            this._residenceRepository = residenceRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.volunteerRepository = volunteerRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _animalRepository.GetAllAnimals();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCommentAsync(int id)
        {


            AddCommentViewModel model = new AddCommentViewModel
            {
                AnimalId = id,
                VolunteerId = 2
            };
            return View(model);
        }

        public async Task<string> getEmailAsync()
        {
            var user = await userManager.GetUserAsync(User);
            return user.Email;
        }

        [HttpGet]
        public IActionResult AddTreatment(int id)
        {

            AddTreatmentViewModel model = new AddTreatmentViewModel
            {
                AnimalId = id,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddTreatment(AddTreatmentViewModel model)
        {
            Animal animal = new Animal();

            if (model.TreatmentType == TreatmentType.CASTRATION || model.TreatmentType == TreatmentType.STERILIZATION)
            {


                if (animal.Age < 0.5)
                {

                    ModelState.AddModelError(nameof(model.TreatmentType), "De behandeling mag pas na de leeftijd van 6 maanden geëxcuteerd worden.");
                }

            }
            if (ModelState.IsValid)
            {

                if (model.AgeRequirement / 12 > (animal.Age))
                {
                    ModelState.AddModelError(nameof(model.AgeRequirement), "Animal does not age requirement.");
                }
                if (model.DateOfTime < animal.DateOfArrival)
                {
                    ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is vóór het moment van arriveren.");
                }

                if (model.DateOfTime > animal.DateOfDeath)
                {
                    ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is na het overlijden van het dier.");

                    switch (model.TreatmentType)
                    {
                        case TreatmentType.VACCINATION:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een omschrijving verplicht.");
                            break;

                        case TreatmentType.OPERATION:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een omschrijving van de type operatie benodigd.");
                            break;

                        case TreatmentType.CHIPPING:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een chipcode (GUID) verplicht.");
                            break;

                        case TreatmentType.EUTHANASIA:
                            ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een beschreven reden nodig.");
                            break;
                    }
                }


                /*animal.IsNeutered == true;*/
                Treatment newTreatment = model.treatment;
                newTreatment.AnimalId = model.AnimalId;

                _treatmentRepository.CreateTreatment(newTreatment);
                return RedirectToAction("details", new { id = newTreatment.AnimalId });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(AddCommentViewModel model)
        {

            string mail = await getEmailAsync();
            var volId = volunteerRepository.GetVolunteerByEmail(mail).VolunteerId;
            model.VolunteerId = volId;
            if (ModelState.IsValid)
            {
                Comment newComment = model.comment;
                newComment.AnimalId = model.AnimalId;
                newComment.VolunteerId = model.VolunteerId;
                newComment.Date = DateTime.Now;

                _commentRepository.CreateComment(newComment);
                return RedirectToAction("details", new { id = newComment.AnimalId });
            }
            return View();
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
            if (model.DateOfBirth == null && model.EstimatedAge == null)
            {
                ModelState.AddModelError(string.Empty, "Choose birthdate or Age estimate");
            }
            if (model.DateOfBirth != null)
            {
                model.Age = calculateAge((DateTime)model.DateOfBirth);
            }
            if (model.DateOfBirth == null && model.EstimatedAge != null)
            {
                model.Age = (int)model.EstimatedAge;
            }

            /*if ((model?.DateOfBirth != null && model?.EstimatedAge != null) || (model?.DateOfBirth == null && model?.EstimatedAge == null))
            {

                ModelState.AddModelError(nameof(model.EstimatedAge), "Geschatte leeftijd óf leeftijd moet ingevuld worden.");
            }*/

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
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
                    AnimalType = model.AnimalType,
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
                    Adoptable = model.Adoptable,
                    AdoptedBy = model.AdoptedBy,
                    ClientNumber = model.ClientNumber,
                    Residence = model.Residence,
                    ResidenceId = model.ResidenceId,
                    Volunteer = model.Volunteer,
                    VolunteerId = model.VolunteerId,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table

                };
                var residence = _residenceRepository.GetResidence((int)model.ResidenceId);


                if (residence == null)
                {
                    newAnimal.ResidenceId = null;
                    _animalRepository.CreateAnimal(newAnimal);
                    return RedirectToAction("details", new { id = newAnimal.Id });
                }
                else
                {
                    if (residence.Animals.Count == residence.Capacity)
                    {
                        ModelState.AddModelError(nameof(model.Residence), "The chosen residence is full.");
                        return View();
                    }
                    else if (model.AnimalType != residence.AnimalType)
                    {
                        ModelState.AddModelError(nameof(model.Residence), "The animal types do not match. Don't try to put a dog in a cat-only residence.");
                        return View();
                    }
                    else
                    {
                        if (model.IsNeutered == true)
                        {
                            if (residence.IsNeutered == true)
                            {
                                _animalRepository.CreateAnimal(newAnimal);
                                return RedirectToAction("details", new { id = newAnimal.Id });
                            }
                            else
                            {
                                ModelState.AddModelError(nameof(model.Residence), "The residence type and given animal are not both neutered.");
                                return View();
                            }
                        }
                        else
                        {
                            if (model.Gender == residence.Gender)
                            {
                                _animalRepository.CreateAnimal(newAnimal);
                                return RedirectToAction("details", new { id = newAnimal.Id });
                            }
                            else
                            { ModelState.AddModelError(nameof(model.Residence), "Not-neutered animals can't be with the opposite gender.");
                                return View();

                            }
                        }
                    }
                }
            }
            return View();
        }
        public int calculateAge(DateTime bday)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - bday.Year;

            if (bday > today.AddYears(-age))
            {
                age--;
            }

            return age;
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
            /*Residence change modelerrors*/
            Animal animal = _animalRepository.GetAnimal(id);
            return View(animal);
        }

        [HttpPost]
        public IActionResult Edit(Animal model)
        {
            if (model.DateOfBirth == null && model.EstimatedAge == null)
            {
                ModelState.AddModelError(string.Empty, "Choose birthdate or Age estimate");
            }
            if (model.DateOfBirth != null)
            {
                model.Age = calculateAge((DateTime)model.DateOfBirth);
            }
            if (model.DateOfBirth == null && model.EstimatedAge != null)
            {
                model.Age = (int)model.EstimatedAge;
            }
            var residence = _residenceRepository.GetResidence((int)model.ResidenceId);

            if (residence.Animals.Count == residence.Capacity)
            {
                ModelState.AddModelError(nameof(model.Residence), "The chosen residence is full.");
                return View();
            }
            else if (model.AnimalType != residence.AnimalType)
            {
                ModelState.AddModelError(nameof(model.Residence), "The animal types do not match. Don't try to put a dog in a cat-only residence.");
                return View();
            }
            else if (model.IsNeutered == false && residence.IsNeutered == false)
            {

                ModelState.AddModelError(nameof(model.Residence), "The residence type and given animal are not both neutered.");
                return View();
            }


            else if (model.Gender != residence.Gender)
            {
                ModelState.AddModelError(nameof(model.Residence), "Not-neutered animals can't be with the opposite gender.");
                return View();
            }


            if (ModelState.IsValid)
            {
                _animalRepository.UpdateAnimal(model);
                return RedirectToAction("index");
            }
            return View(model);
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

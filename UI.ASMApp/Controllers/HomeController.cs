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
            _animalRepository = animalRepository;
            _commentRepository = commentRepository;
            _treatmentRepository = treatmentRepository;
            _residenceRepository = residenceRepository;
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

            Animal animal = _animalRepository.GetAnimal(model.AnimalId);

            if (animal.Age == 0)
            {

                DateTime date1 = (DateTime)animal.DateOfBirth;
                var date2 = DateTime.Now;

                var monthscalculated = (date1.Year - date2.Year) * 12 + date2.Month - date1.Month;
                TimeSpan dayscalculated = date2 - date1;

                var totaldays = dayscalculated.TotalDays;

                if (monthscalculated < 6 && (int)model.treatment.TreatmentType == 0 || (int)model.treatment.TreatmentType == 1 && model.treatment.AgeRequirement < 6)
                {
                    ModelState.AddModelError(string.Empty, "Het dier moet minimaal 6 maanden oud zijn voor deze behandeling.");
                }

            }

           

                if ((int)model.treatment.TreatmentType == 0 || (int)model.treatment.TreatmentType == 1)
                {
                    animal.IsNeutered = true;
                    _animalRepository.UpdateAnimal(animal);
                }

                if (model.AgeRequirement / 12 > (animal.Age))
                {
                    ModelState.AddModelError(nameof(model.AgeRequirement), "Het dier voldoet niet aan de leeftijdseisen.");
                }
                if (model.treatment.DateOfTime < animal.DateOfArrival)
                {
                    ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is vóór het moment van arriveren.");
                }

            if (model.treatment.DateOfTime > animal.DateOfDeath)
            {
                ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is na het overlijden van het dier.");
            }


               if ((int)model.treatment.TreatmentType == 2 &&
                    model.treatment.Description == null) {

                        ModelState.AddModelError(string.Empty, "Bij deze behandeling is een omschrijving verplicht.");
      
                            }

                if ((int)model.treatment.TreatmentType == 3 &&
                     model.treatment.Description == null)
                {

                    ModelState.AddModelError(string.Empty, "Bij deze behandeling is een omschrijving van de type operatie benodigd.");

                }

                if ((int)model.treatment.TreatmentType == 4 &&
                    model.treatment.Description == null)
                {

                    ModelState.AddModelError(string.Empty, "Bij deze behandeling is een chipcode (GUID) verplicht.");

                }

                if ((int)model.treatment.TreatmentType == 5 &&
                    model.treatment.Description == null)
                {

                    ModelState.AddModelError(string.Empty, "Bij deze behandeling is een beschreven reden nodig.");

                }



            if (ModelState.IsValid)
            {

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
            Volunteer vol = volunteerRepository.GetVolunteer(volId);
            model.VolunteerId = volId;
            if (ModelState.IsValid)
            {
                Comment newComment = model.comment;
                newComment.AnimalId = model.AnimalId;
                newComment.VolunteerId = model.VolunteerId;
                newComment.CommentMadeBy = vol;
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

            if (model.DateOfBirth != null && model.EstimatedAge != null)
            {
                ModelState.AddModelError(string.Empty, "Cannot enter both estimated age and date of birth.");
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

        [HttpPost]
        public IActionResult DeleteTreatment(int id)
        {

            _treatmentRepository.DeleteTreatment(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            /*Residence change modelerrors*/
            Animal animal = _animalRepository.GetAnimal(id);
            return View(animal);
        }


        [HttpGet]
        public ViewResult EditTreatment(int id)
        {


            Treatment treatment = _treatmentRepository.GetTreatment(id);

            AddTreatmentViewModel model = new AddTreatmentViewModel
            {
                treatment = treatment,
                AnimalId = treatment.AnimalId
            };
            return View(model);       
        }


        [HttpPost]
        public IActionResult Edit(Animal model)
        {
            if (model.DateOfBirth == null && model.EstimatedAge == null)
            {
                ModelState.AddModelError(string.Empty, "Choose birthdate or Age estimate");
            }

            if (model.DateOfBirth != null && model.EstimatedAge != null)
            {
                ModelState.AddModelError(string.Empty, "Cannot enter both estimated age and date of birth.");
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
             if (model.AnimalType != residence.AnimalType)
            {
                ModelState.AddModelError(nameof(model.Residence), "The animal types do not match. Don't try to put a dog in a cat-only residence.");
                return View();
            }
             if (model.IsNeutered == false && residence.IsNeutered == false)
            {

                ModelState.AddModelError(nameof(model.Residence), "The residence type and given animal are not both neutered.");
                return View();
            }


             if (model.Gender != residence.Gender)
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



        [HttpPost]
        public IActionResult EditTreatment(AddTreatmentViewModel model)
        {

            Animal animal = _animalRepository.GetAnimal(model.AnimalId);

            if (animal.Age == 0)
            {

                DateTime date1 = (DateTime)animal.DateOfBirth;
                var date2 = DateTime.Now;

                var monthscalculated = (date1.Year - date2.Year) * 12 + date2.Month - date1.Month;
                TimeSpan dayscalculated = date2 - date1;

                var totaldays = dayscalculated.TotalDays;

                if (monthscalculated < 6 && (int)model.treatment.TreatmentType == 0 || (int)model.treatment.TreatmentType == 1 && model.treatment.AgeRequirement < 6)
                {
                    ModelState.AddModelError(string.Empty, "Het dier moet minimaal 6 maanden oud zijn voor deze behandeling.");
                }

            }



            if ((int)model.treatment.TreatmentType == 0 || (int)model.treatment.TreatmentType == 1)
            {
                animal.IsNeutered = true;
                _animalRepository.UpdateAnimal(animal);
            }

            if (model.AgeRequirement / 12 > (animal.Age))
            {
                ModelState.AddModelError(nameof(model.AgeRequirement), "Het dier voldoet niet aan de leeftijdseisen.");
            }
            if (model.treatment.DateOfTime < animal.DateOfArrival)
            {
                ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is vóór het moment van arriveren.");
            }

            if (model.treatment.DateOfTime > animal.DateOfDeath)
            {
                ModelState.AddModelError(nameof(model.DateOfTime), "Datum van behandeling is na het overlijden van het dier.");
            }


            if ((int)model.treatment.TreatmentType == 2 &&
                 model.treatment.Description == null)
            {

                ModelState.AddModelError(string.Empty, "Bij deze behandeling is een omschrijving verplicht.");

            }

            if ((int)model.treatment.TreatmentType == 3 &&
                 model.treatment.Description == null)
            {

                ModelState.AddModelError(string.Empty, "Bij deze behandeling is een omschrijving van de type operatie benodigd.");

            }

            if ((int)model.treatment.TreatmentType == 4 &&
                model.treatment.Description == null)
            {

                ModelState.AddModelError(string.Empty, "Bij deze behandeling is een chipcode (GUID) verplicht.");

            }

            if ((int)model.treatment.TreatmentType == 5 &&
                model.treatment.Description == null)
            {

                ModelState.AddModelError(string.Empty, "Bij deze behandeling is een beschreven reden nodig.");

            }



            if (ModelState.IsValid)
            {
                Treatment newTreatment = model.treatment;
                newTreatment.AnimalId = model.AnimalId;

                _treatmentRepository.UpdateTreatment(newTreatment);
                return RedirectToAction("details", new { id = newTreatment.AnimalId });
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

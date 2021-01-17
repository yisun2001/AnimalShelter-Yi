using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Managment.Infrastructure;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Managment.Portal.Controllers
{
    public class TreatmentController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITreatmentServices _treatmentServices;

        public TreatmentController(
            ILogger<HomeController> logger, 
            IAnimalRepository animalRepository, 
            INoteRepository noteRepository, 
            IResidenceRepository residenceRepository, 
            ITreatmentRepository treatmentRepository, 
            ITreatmentServices treatmentServices)
        {
            _logger = logger;
            _animalRepository = animalRepository;
            _noteRepository = noteRepository;
            _residenceRepository = residenceRepository;
            _treatmentRepository = treatmentRepository;
            _treatmentServices = treatmentServices;
        }
        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult TreatmentsOverview(int Id)
        {
            return View(_treatmentRepository.GetByAnimalId(Id).ToViewModel());
        }
        [Authorize(Policy = "VolunteerOnly")]
        [HttpGet]
        public IActionResult AddTreatment()
        {
            var model = new AddTreatmentViewModel();

            PrefillSelectOptions();
            return View(model);
        }

        private void PrefillSelectOptions()
        {
           
            var types = Enum.GetValues(typeof(TreatmentType)).Cast<TreatmentType>();
            ViewBag.Types = new SelectList(types, "Type");

        }
        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public async Task<IActionResult> AddTreatment(AddTreatmentViewModel newtreatment, int Id)
        {
            try {
               
                    Treatment treatmentToCreate = new Treatment
                    {
                        Type = newtreatment.Type,
                        AllowedAgeInMonths = newtreatment.AllowedAgeInMonths,
                        Animal = newtreatment.Animal,
                        Charge = newtreatment.Charge,
                        AnimalId = Id,
                        Description = newtreatment.Description,
                        PerformedBy = newtreatment.PerformedBy,
                        PerformedOn = (DateTime)newtreatment.PerformedOn
                    };
                    if (ModelState.IsValid)
                        {
                    await _treatmentServices.AddTreatment(treatmentToCreate);
                    return RedirectToAction("TreatmentsOverview", new { id = Id });

                }
            }
            catch (InvalidOperationException e) {
                ModelState.AddModelError("", e.Message);
            }
            
            PrefillSelectOptions();

            return View();
        }
        [Authorize(Policy = "VolunteerOnly")]
        public async Task<IActionResult> DeleteTreatment(int Id)
        {
            var treatment = _treatmentRepository.GetById(Id);
            await _treatmentRepository.DeleteTreatment(treatment);
            return RedirectToAction("TreatmentsOverview", new { id = treatment.AnimalId });
        }
        [Authorize(Policy = "VolunteerOnly")]
        [HttpGet]
        public async Task<IActionResult> UpdateTreatment(int Id)
        {
            var treatment = _treatmentRepository.GetById(Id);


            var model = new UpdateTreatmentViewModel()
            {
                Id = treatment.Id,
                Type = treatment.Type,
                AnimalId = treatment.AnimalId,
                AllowedAgeInMonths = treatment.AllowedAgeInMonths,
                Charge = treatment.Charge,
                Description = treatment.Description,
                PerformedBy = treatment.PerformedBy,
                PerformedOn = treatment.PerformedOn,
                //Animal = treatment.Animal
            };

            PrefillSelectOptions();

            return View(model);
        }
        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public async Task<IActionResult> UpdateTreatment(UpdateTreatmentViewModel treatment)
        {

            try
            {
                var animal = _animalRepository.GetById(treatment.AnimalId);
                var treatmentToUpdate = _treatmentRepository.GetById(treatment.Id);

                if (ModelState.IsValid)
                {

                    treatmentToUpdate.Animal = animal;
                    treatmentToUpdate.Type = treatment.Type;
                    treatmentToUpdate.AllowedAgeInMonths = treatment.AllowedAgeInMonths;
                    treatmentToUpdate.Charge = treatment.Charge;
                    treatmentToUpdate.Description = treatment.Description;
                    treatment.PerformedBy = treatment.PerformedBy;
                    treatment.PerformedOn = treatment.PerformedOn;

                    await _treatmentServices.UpdateTreatment(treatmentToUpdate);

                    return RedirectToAction("TreatmentsOverview", new { id = treatment.AnimalId });
                }
            }
            catch (InvalidOperationException e){
                ModelState.AddModelError("", e.Message);
            }
           

            PrefillSelectOptions();
            return View(treatment);
        }
    }
}

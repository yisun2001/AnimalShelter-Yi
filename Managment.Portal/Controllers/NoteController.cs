using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Managment.Infrastructure;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Managment.Portal.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IAnimalServices _animalServices;
        private readonly IVolunteerRepository _volunteerRepository;
        private SignInManager<IdentityUser> _signInManager;

        public NoteController(
            ILogger<HomeController> logger,
            IAnimalRepository animalRepository,
            INoteRepository noteRepository,
            IResidenceRepository residenceRepository,
            ITreatmentRepository treatmentRepository,
            IAnimalServices animalServices,
            IVolunteerRepository volunteerRepository,
            SignInManager<IdentityUser> signInManager
            )
        {
            _logger = logger;
            _animalRepository = animalRepository;
            _noteRepository = noteRepository;
            _residenceRepository = residenceRepository;
            _treatmentRepository = treatmentRepository;
            _animalServices = animalServices;
            _volunteerRepository = volunteerRepository;
            _signInManager = signInManager;
        }

        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult NotesOverview(int Id)
        {
            return View(_noteRepository.GetByAnimalId(Id).ToViewModel());
        }
        [Authorize(Policy = "VolunteerOnly")]
        [HttpGet]
        public IActionResult AddNote()
        {
            var model = new AddNoteViewModel();

            return View(model);
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public async Task<IActionResult> AddNote(AddNoteViewModel note, int Id)
        {
            _signInManager.IsSignedIn(User);
            var email = User.Identity.Name;
            var user = _volunteerRepository.GetVolunteerByEmail(email);
            if (ModelState.IsValid)
            {
                Note noteToCreate = new Note
                {
                    Comment = note.Comment,
                    Animal = note.Animal,
                    AnimalId = Id,
                    MadeBy = $"{user.FirstName} {user.LastName}",
                    WrittenOn = DateTime.Now,
                    VolunteerId = user.Id
                };

                await _noteRepository.AddNote(noteToCreate);

                return RedirectToAction("NotesOverview", new { id = Id });
            }

            return View();
        }
        [Authorize(Policy = "VolunteerOnly")]
        public async Task<IActionResult> DeleteNote(int Id)
        {
            Note note = await _noteRepository.GetById(Id);
            await _noteRepository.DeleteNote(note);
            return RedirectToAction("NotesOverview", new { id = note.AnimalId });
        }
    }
}

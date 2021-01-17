using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Managment.Portal.Models;
using Managment.Core.DomainServices;
using Managment.Infrastructure;

namespace Managment.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IAnimalServices _animalServices;

        public HomeController(
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

        public IActionResult Index()
        {
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

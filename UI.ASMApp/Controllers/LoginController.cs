using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Core.Domain;
using Core.DomainServices;
using Microsoft.Extensions.Logging;
using UI.ASMApp.Models;

namespace UI.ASMApp.Controllers
{
    public class LoginController : Controller 
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;

        public LoginController(ILogger<HomeController> logger, IAnimalRepository animalRepository)
        {
            _logger = logger;
            this._animalRepository = animalRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        { return View();
        }
    }
}


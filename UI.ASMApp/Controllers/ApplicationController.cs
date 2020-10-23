using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UI.ASMApp.Controllers
{
    [Authorize(Policy = "Volunteer")]
    public class ApplicationController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationController(ILogger<HomeController> logger, IApplicationRepository applicationRepository)
        {
            this.logger = logger;
            this._applicationRepository = applicationRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _applicationRepository.GetAllApplications();
            return View(model);
            
        }
    }
}

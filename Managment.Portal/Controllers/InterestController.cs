using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.DomainServices;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Managment.Portal.Controllers
{
    public class InterestController : Controller
    {
        private readonly IInterestRepository _interestRepository;

        public InterestController(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }
        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult Overview()
        {
            return View(_interestRepository.GetAll().OrderByDescending(g => g.AddedOn).ToViewModel());
        }
    }
}

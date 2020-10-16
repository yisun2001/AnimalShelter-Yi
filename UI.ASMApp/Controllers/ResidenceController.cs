

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.ASMApp.Models;
using System.Diagnostics;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using EF.Infrastructure;

namespace UI.ASMApp.Controllers
{


    public class ResidenceController : Controller
    {

        private readonly ILogger<ResidenceController> _logger;
        private readonly IResidenceRepository _residenceRepository;

        public ResidenceController(ILogger<ResidenceController> logger, IResidenceRepository residenceRepository)
        {
            _logger = logger;
            this._residenceRepository = residenceRepository;
        }

        // GET: ResidenceController
        [HttpGet]
        public ActionResult Index()
        {
            List<Residence> lists = _residenceRepository.GetAllResidences().ToList();
            var model = lists;
            return View(model);
        }

        // GET: ResidenceController/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _residenceRepository.GetResidence(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        // GET: ResidenceController/Create
        [HttpPost]
        public ActionResult Create(CreateResidenceViewModel model)
        {
            if (ModelState.IsValid)
            {


                Residence newResidence = new Residence
                {

                    Shelter = model.Shelter,
                    Capacity = model.Capacity,
                    MaxCapacity = model.MaxCapacity,
                    AnimalType = model.AnimalType,
                    IsNeutered = model.IsNeutered,
                    IsIndivudialResidence = model.IsIndivudialResidence,
                    Animals = model.Animals,

                };

                _residenceRepository.CreateResidence(newResidence);
                return RedirectToAction("details", new { id = newResidence.Id });
            }


            return View();
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            _residenceRepository.DeleteResidence(id);
            return RedirectToAction("index");
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Residence residence = _residenceRepository.GetResidence(id);
            return View(residence);
        }

        [HttpPost]
        public IActionResult Edit(Residence residence)
        {
            if (ModelState.IsValid)
            {
                _residenceRepository.UpdateResidence(residence);
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
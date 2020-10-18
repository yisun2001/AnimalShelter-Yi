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
using System.Net;
using System.Linq.Expressions;

namespace UI.ASMApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _commentRepository;
        private readonly IAnimalRepository _animalRepository;
        public CommentController(ILogger<CommentController> logger, ICommentRepository commentRepository, IAnimalRepository animalRepository)
        {
            _logger = logger;
            this._commentRepository = commentRepository;
            this._animalRepository = animalRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Comment> lists = _commentRepository.GetAllComments().ToList();
            var model = lists;
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _commentRepository.GetComment(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        /*[HttpPost]
        public IActionResult Create(Treatment model, Animal animal)
        {
            if (ModelState.IsValid) { 
          
                Treatment treatment = new Treatment
                {

                    TypeOfTreatment = model.TypeOfTreatment,
                    Description = model.Description,
                    Costs = model.Costs,
                    AgeRequirement = model.AgeRequirement,
                    DateOfTime = model.DateOfTime,
                    Animal = model.Animal,
                   
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table

                };
                animal.Treatments.Add(treatment);

                return RedirectToAction("details", new { id = animal.Id });
            }

            return View();
        }*/

        [HttpPost]
        public IActionResult Create(Comment model, int Id)
        {

            var animal = _animalRepository.GetAnimal(Id);

            if (ModelState.IsValid)
            {


                Comment newComment = new Comment
                {
                    CommentId = model.CommentId,
                    Animal = model.Animal,
                    AnimalId = model.AnimalId,
                    CommentMadeBy = model.CommentMadeBy,
                    ClientNumber = model.ClientNumber,
                    CommentText = model.CommentText,
                    DateTime = model.DateTime,

                };
                try
                {
                    _commentRepository.CreateComment(newComment, Id);
                    return RedirectToAction("details", new { id = newComment.CommentId });
                }
                catch { return View();}
            }

            return View();
        }



/*
        [HttpPost]
        public IActionResult Delete(int id)
        {

            _commentRepository.DeleteComment(id);
            return RedirectToAction("index");
        }*/


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Comment comment = _commentRepository.GetComment(id);
            return View(comment);
        }

        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _commentRepository.UpdateComment(comment);
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

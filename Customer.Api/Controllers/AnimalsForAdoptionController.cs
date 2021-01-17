using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnimalsForAdoptionController : ControllerBase
    {
        private readonly IAnimalForAdoptionRepository _animalForAdoptionRepository;

        public AnimalsForAdoptionController(IAnimalForAdoptionRepository animalForAdoptionRepository)
        {
            _animalForAdoptionRepository = animalForAdoptionRepository;
        }

        [HttpGet]
        public ActionResult<AnimalForAdoption> Get()
        {
            return Ok(_animalForAdoptionRepository.GetAllAnimals());
        }
        // <param name="id"> </params/>
        [HttpGet("{id}")]
        public ActionResult<AnimalForAdoption> Get(int id)
        {
            return _animalForAdoptionRepository.GetById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnimalForAdoption animal)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                AnimalForAdoption animalToCreate = new AnimalForAdoption
                {
                    Name =animal.Name,
                    AddedOn = DateTime.Now,
                    Sterilised = animal.Sterilised,
                    ReasonAdoptable = animal.ReasonAdoptable,
                    CustomerId = animal.CustomerId,
                    Gender = animal.Gender,
                    Type = animal.Type,
                    Customer = animal.Customer
                };

                _animalForAdoptionRepository.AddAnimal(animalToCreate);
                result = Ok(animalToCreate);
            }
            else
            {
                result = BadRequest(ModelState);
            }
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] AnimalForAdoption prod)
        {
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Customer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        // GET api/v1/animals
        [HttpGet]
        public ActionResult<Animal> Get([FromQuery] string Type, [FromQuery] string Gender, [FromQuery] string KidFriendly, [FromQuery] string adoptable) {
            return Ok(_animalRepository.GetAllAnimalsAdoptable(Type, Gender, KidFriendly, adoptable));
        }
        // <param name="id"> </params/>
        [HttpGet("{id}")]
        public ActionResult<Animal> Get(int id) {
            return _animalRepository.GetById(id);
        }

        [HttpPost]
        public IActionResult Post(Animal aNewAnimal)
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Animal prod)
        {
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete() {
            return BadRequest();
        }
    }
}

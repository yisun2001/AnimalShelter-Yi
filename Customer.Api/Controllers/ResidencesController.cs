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
    public class ResidencesController : ControllerBase
    {
        private readonly IResidenceRepository _residenceRepository;

        public ResidencesController(IResidenceRepository residenceRepository)
        {
            _residenceRepository = residenceRepository;
        }

        [HttpGet]
        public ActionResult<Residence> Get([FromQuery] string Type)
        {
            return Ok(_residenceRepository.GetAllResidences(Type));
        }


        [HttpPost]
        public IActionResult Post(Residence aNewAnimal)
        {
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Residence prod)
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

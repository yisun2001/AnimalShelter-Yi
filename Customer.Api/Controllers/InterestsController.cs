using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestRepository _interestRepository;

        public InterestsController(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        [HttpGet]
        public ActionResult<Interest> Get([FromQuery] string customerId)
        {
            return Ok(_interestRepository.GetAllInterests(customerId));
        }
        // <param name="id"> </params/>

        [HttpPost]
        public IActionResult Post([FromBody] Interest interest)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                Interest interestToCreate = new Interest
                {
                    AnimalId = interest.AnimalId,
                    AddedOn = DateTime.Now,
                    Animal = interest.Animal,
                    Customer = interest.Customer,
                    CustomerId = interest.CustomerId,
                    Comment = interest.Comment
                };

                _interestRepository.AddInterest(interestToCreate);
                result = Ok(interestToCreate);
            }
            else
            {
                result = BadRequest(ModelState);
            }
            return result;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            Interest interest = _interestRepository.GetById(id);
            _interestRepository.DeleteInterest(interest);
        
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Interest prod)
        {
            return BadRequest();
        }
    }
}

using Managment.Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class UpdateAnimalViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? EstimatedAge { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public AnimalType Type { get; set; }

        public string Race { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        public DateTime? DateOfAdoption { get; set; }

        public DateTime? DateOfPassing { get; set; }

        [Required]
        public bool Sterilised { get; set; }

        [Required]
        public KidFriendly KidFriendly { get; set; }

        [Required]
        public string ReasonAdoptable { get; set; }

        [Required]
        public bool Adoptable { get; set; }

        public string AdoptedBy { get; set; }

        public Residence Residence { get; set; }

        public int ResidenceId { get; set; }
    }
}

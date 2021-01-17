using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Portal.Models
{
    public class AnimalForAdoptionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AnimalType Type { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public bool Sterilised { get; set; }

        [Required]
        public string ReasonAdoptable { get; set; }

        public Managment.Core.Domain.Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

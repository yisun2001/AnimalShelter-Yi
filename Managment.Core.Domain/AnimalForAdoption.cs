using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Managment.Core.Domain
{
    public class AnimalForAdoption
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

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

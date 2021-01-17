using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class AnimalViewModel
    {
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? EstimatedAge { get; set; }

        public string Description { get; set; }

        public AnimalType Type { get; set; }

        public string Race { get; set; }

        public Gender Gender { get; set; }

        public string Image { get; set; }

        public DateTime DateOfArrival { get; set; }

        public DateTime? DateOfAdoption { get; set; }

        public DateTime? DateOfPassing { get; set; }

        public bool Sterilised { get; set; }

        public char KidFriendly { get; set; }

        public string ReasonAdoptable { get; set; }

        public bool Adoptable { get; set; }

        public string AdoptedBy { get; set; }

        public Residence Residence { get; set; }

        public int ResidenceId { get; set; }
    }
}

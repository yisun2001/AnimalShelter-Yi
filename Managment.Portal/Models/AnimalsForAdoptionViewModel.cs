using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class AnimalsForAdoptionViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public AnimalType Type { get; set; }


        public Gender Gender { get; set; }


        public bool Sterilised { get; set; }


        public string ReasonAdoptable { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

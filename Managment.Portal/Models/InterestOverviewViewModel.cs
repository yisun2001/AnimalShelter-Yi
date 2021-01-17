using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class InterestOverviewViewModel
    {
        public int Id { get; set; }

        public Animal Animal { get; set; }

        public int AnimalId { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public string Comment { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

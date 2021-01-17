using System;
using System.Collections.Generic;
using System.Text;

namespace Managment.Core.Domain
{
    public class Interest
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

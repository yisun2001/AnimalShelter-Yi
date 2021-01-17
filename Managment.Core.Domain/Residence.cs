using System;
using System.Collections.Generic;
using System.Text;

namespace Managment.Core.Domain
{
    public class Residence
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public AnimalType? Type {get; set;}

        public Gender? Gender { get; set; }

    }
}

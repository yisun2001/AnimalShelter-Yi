using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Residence
    {
        public int Id { get; set; }
        public string Shelter { get; set; }
        public int Capacity { get; set; }
        public string AnimalType { get; set; }
        public bool IsNeutered { get; set; }
        public string IndividualOrGroup { get; set; }
        public string Gender { get; set; }

        public ICollection<Animal> Animals { get; set; }


    }
}

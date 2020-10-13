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
        public int MaxCapacity { get; set; }
        public string AnimalType { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsIndivudialResidence { get; set; }


        public ICollection<Animal> Animals{ get; set; }


    }
}

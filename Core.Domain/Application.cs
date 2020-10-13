using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Application
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public int ClientNumber { get; set; }
        public string AnimalName { get; set; }
        public string TypeOfAnimal { get; set; }
        public bool IsNeutered { get; set; }
    }
}

﻿using System;
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


        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public bool AddAnimal(Animal animal) {
            if (Capacity < MaxCapacity) {

                Animals.Add(animal);
                Capacity++; 
                   return true;  }
            else return false;
        }

        public void RemoveAnimal(Animal animal)
        {
           

                Animals.Remove(animal);
                Capacity--;
       
        }


    }
}

using Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace UI.ASMApp.Models
{
    public class CreateResidenceViewModel
    {
        [Required]
        public string Shelter { get; set; }
        public int Capacity { get; set; }
        public int MaxCapacity { get; set; }
        public string AnimalType { get; set; }
        public bool IsNeutered { get; set; }
        public bool IsIndivudialResidence { get; set; }


        public ICollection<Animal> Animals { get; set; }
    }
}

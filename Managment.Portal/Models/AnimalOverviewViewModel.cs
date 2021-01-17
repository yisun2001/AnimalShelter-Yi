using Managment.Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class AnimalOverviewViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? EstimatedAge { get; set; }
        public AnimalType Type { get; set; }
        public string Race { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }
        public bool Adoptable { get; set; }
        public Residence Residence { get; set; }
        public int ResidenceId { get; set; }
    }
}

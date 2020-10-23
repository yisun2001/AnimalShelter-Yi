using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ASMApp.Models
{
    public class AddTreatmentViewModel
    {

        public int Id { get; set; }
        public Treatment treatment { get; set; }

        public int AnimalId { get; set; }

        public TreatmentType TreatmentType { get; set; }

        public int AgeRequirement { get; set; }

        public Animal Animal { get; set; }

        public string Description { get; set; }

        public DateTime DateOfTime { get; set; }
    }
}

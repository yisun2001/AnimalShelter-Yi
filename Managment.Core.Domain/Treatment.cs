using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Managment.Core.Domain
{
    public class Treatment
    {
        public int Id { get; set; }
        [Required]
        public TreatmentType Type { get; set; }

        public string Description { get; set; }

        public double? Charge { get; set; }

        public int? AllowedAgeInMonths { get; set; }
        [Required]
        public string PerformedBy { get; set; }
        [Required]
        public DateTime PerformedOn { get; set; }

        public Animal Animal { get; set; }

        public int AnimalId { get; set; }
    }
}

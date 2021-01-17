using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class UpdateTreatmentViewModel
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

        public int AnimalId { get; set; }

        public Animal Animal { get; set; }

    }
}

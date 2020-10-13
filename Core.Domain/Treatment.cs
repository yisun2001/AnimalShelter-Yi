﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public class Treatment
    {
        public int Id { get; set; }
        public TypeOfTreatment TypeOfTreatment { get; set; }
        public string Description { get; set; }

        [Column(TypeName ="decimal")]
        public decimal Costs { get; set; }
        public int AgeRequirement { get; set; }
        public DateTime DateOfTime { get; set; }

        public Animal Animal { get; set; }
        public int AnimalId { get; set; }
    }
}

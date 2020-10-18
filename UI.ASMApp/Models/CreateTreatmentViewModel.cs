using Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace UI.ASMApp.Models
{
    public class CreateTreatmentViewModel
    {

        public TypeOfTreatment TypeOfTreatment { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Costs { get; set; }

        public Client TreatmentExecutedby { get; set; }
        public int AgeRequirement { get; set; }
        public DateTime DateOfTime { get; set; }

        public Animal Animal { get; set; }
        public int AnimalId { get; set; }
    }
    }







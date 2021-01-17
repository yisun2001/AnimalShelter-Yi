using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class NotesOverviewViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime WrittenOn { get; set; }
        [Required]
        public string MadeBy { get; set; }

        public Animal Animal { get; set; }

        public int AnimalId { get; set; }

    }
}

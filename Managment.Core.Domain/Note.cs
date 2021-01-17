using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Managment.Core.Domain
{
    public class Note
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

        public Volunteer Volunteer { get; set; }

        public int VolunteerId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Animal> Animals { get; set; }

    }
}

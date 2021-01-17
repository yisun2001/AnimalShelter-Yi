using System;
using System.Collections.Generic;
using System.Text;

namespace Managment.Core.Domain
{
    public class Volunteer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; }

        ICollection<Note> Notes { get; set; }
    }
}

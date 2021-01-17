using System;
using System.Collections.Generic;
using System.Text;

namespace Managment.Core.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public ICollection<Interest> Interests { get; set; }

        public ICollection<AnimalForAdoption> AnimalForAdoptions { get; set; }
    }
}

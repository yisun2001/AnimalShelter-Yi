using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Client
    {
        public int ClientNumber { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Animal> Animals { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Application> Applications { get; set; }

        public Cart Cart { get; set; }

        public int CartId { get; set; }
    }
}

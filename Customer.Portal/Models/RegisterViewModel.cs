using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Portal.Models
{
    public class RegisterViewModel
    {
        [JsonProperty("firstName")]
        [Required]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        [Required]
        public string LastName { get; set; }
        [JsonProperty("email")]
        [Required][EmailAddress]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
        [JsonProperty("dateOfBirth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("telephoneNumber")]
        [Required][Phone]
        public string TelephoneNumber { get; set; }
        [JsonProperty("postCode")]
        [Required]
        public string PostCode { get; set; }
        [JsonProperty("city")]
        [Required]
        public string City { get; set; }
        [JsonProperty("street")]
        [Required]
        public string Street { get; set; }
        [JsonProperty("houseNumber")]
        [Required]
        public string HouseNumber { get; set; }

    }
}

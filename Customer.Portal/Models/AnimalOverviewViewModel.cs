using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Portal.Models
{
    public class AnimalOverviewViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? EstimatedAge { get; set; }

        public string Description { get; set; }

        public AnimalType Type { get; set; }

        public string Race { get; set; }

        public Gender Gender { get; set; }

        public byte[] Image { get; set; }

        public DateTime DateOfArrival { get; set; }

        public bool Sterilised { get; set; }

        public KidFriendly KidFriendly { get; set; }

        public bool Adoptable { get; set; }

        public string AdoptedBy { get; set; }

        public int ResidenceId { get; set; }

        public int GetAnimalAge()
        {

            int _age;
            if (DateOfBirth == null)
            {
                return (int)EstimatedAge;
            }
            else
            {
                DateTime datenow = DateTime.Now;
                if (datenow.Month > DateOfBirth.Value.Month)
                {
                    _age = (datenow.Year - DateOfBirth.Value.Year);
                }
                else if (datenow.Month < DateOfBirth.Value.Month)
                {
                    _age = (datenow.Year - DateOfBirth.Value.Year) - 1;
                }
                else
                {
                    if (datenow.Day >= DateOfBirth.Value.Day)
                    {
                        _age = (datenow.Year - DateOfBirth.Value.Year);
                    }
                    else
                    {
                        _age = (datenow.Year - DateOfBirth.Value.Year) - 1;
                    }
                }
                return _age;
            }

        }
    }
}

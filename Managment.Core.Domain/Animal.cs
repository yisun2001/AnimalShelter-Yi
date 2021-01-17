using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Managment.Core.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? EstimatedAge { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public AnimalType Type { get; set; }

        public string Race { get; set; }
        [Required]
        public Gender Gender { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        public DateTime? DateOfAdoption { get; set; }

        public DateTime? DateOfPassing { get; set; }

        [Required]
        public bool Sterilised { get; set; }

        [Required]
        public KidFriendly KidFriendly { get; set; }

        public ICollection<Treatment> Treatments { get; set; }

        public ICollection<Note> Notations { get; set; }

        [Required]
        public string ReasonAdoptable { get; set; }

        [Required]
        public bool Adoptable { get; set; }

        public string AdoptedBy { get; set; }

        public Residence Residence { get; set; }

        public int ResidenceId { get; set; }

        public ICollection<Interest> Interests { get; set; }

        public int GetAnimalAge() 
        {

            int _age;
                if (DateOfBirth == null)
                {
                   return (int )EstimatedAge;
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

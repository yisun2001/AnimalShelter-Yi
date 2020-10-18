using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace Core.Domain
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int Age { get; set; }

        public int? EstimatedAge { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string TypeOfAnimal { get; set; }

        public string Breed { get; set; }
        [Required]
        public string Gender { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }
        public DateTime? DateOfAdoption { get; set; }
        public DateTime? DateOfDeath { get; set; }
        [Required]
        public bool IsNeutered { get; set; }
        [Required]
        public bool CompatibleWithKids { get; set; }
        //[Required]
        public string ReasonOfDistancing { get; set; }

     /*   public bool Adoptable { get; set; }*/

        public ICollection<Treatment> Treatments { get; set; }

        public void AddTreatment(Treatment treatment)
        {

            Treatments.Add(treatment);
        }



        public ICollection<Comment> Comments { get; set; }


        public void AddComment(Comment comment)
        {

            Comments.Add(comment);
        }


        public Client AdoptedBy { get; set; }
        public int? ClientNumber { get; set; }
        public Residence Residence { get; set; }
        public int? ResidenceId { get; set; }

        public Volunteer Volunteer { get; set; }
        public int? VolunteerId { get; set; }

       /* public void CalculateAge() {
            DateTime n = DateTime.Now;
            int age = n.Year - DateOfBirth.Year;
            int months = age * 12;

            if (n.Month < DateOfBirth.Month || (n.Month == DateOfBirth.Month && n.Day < DateOfBirth.Day)) {
                age--;
                Age = age;

            }
        }*/
   }
    }

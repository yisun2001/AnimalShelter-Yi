using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public static class ViewModelHelper
    {
        public static List<AnimalOverviewViewModel> ToViewModel(this IEnumerable<Animal> animals) {
            var result = new List<AnimalOverviewViewModel>();

            foreach (var animal in animals) {
                result.Add(animal.ToViewModel());
            }

            return result;
        }

        public static AnimalOverviewViewModel ToViewModel(this Animal animal) 
        {
            var result = new AnimalOverviewViewModel{
                Name = animal.Name,
                Age = animal.GetAnimalAge(),
                DateOfBirth = animal.DateOfBirth,
                EstimatedAge = animal.EstimatedAge,
                Type = animal.Type,
                Race = animal.Race,
                Gender = animal.Gender,
                Adoptable = animal.Adoptable,
                Image = animal.Image,
                Residence = animal.Residence,
                ResidenceId = animal.ResidenceId,
                Id = animal.Id

            };
            return result;
        }

        public static List<TreatmentOverviewViewModel> ToViewModel(this IEnumerable<Treatment> treatments)
        {
            var result = new List<TreatmentOverviewViewModel>();

            foreach (var treatment in treatments)
            {
                result.Add(treatment.ToViewModel());
            }

            return result;
        }

        public static TreatmentOverviewViewModel ToViewModel(this Treatment treatment)
        {
            var result = new TreatmentOverviewViewModel
            {
                Id = treatment.Id,
                Type = treatment.Type,
                AllowedAgeInMonths = treatment.AllowedAgeInMonths,
                Animal = treatment.Animal,
                AnimalId = treatment.AnimalId,
                Charge = treatment.Charge,
                Description = treatment.Description,
                PerformedBy = treatment.PerformedBy,
                PerformedOn = treatment.PerformedOn

            };
            return result;
        }



        public static List<NotesOverviewViewModel> ToViewModel(this IEnumerable<Note> Notes)
        {
            var result = new List<NotesOverviewViewModel>();

            foreach (var note in Notes)
            {
                result.Add(note.ToViewModel());
            }

            return result;
        }

        public static NotesOverviewViewModel ToViewModel(this Note note)
        {
            var result = new NotesOverviewViewModel
            {
                Id = note.Id,
                Animal = note.Animal,
                AnimalId = note.AnimalId,
                Comment = note.Comment,
                MadeBy = note.MadeBy,
                WrittenOn = note.WrittenOn

            };
            return result;
        }

        public static List<InterestOverviewViewModel> ToViewModel(this IEnumerable<Interest> Interests)
        {
            var result = new List<InterestOverviewViewModel>();

            foreach (var interest in Interests)
            {
                result.Add(interest.ToViewModel());
            }

            return result;
        }

        public static InterestOverviewViewModel ToViewModel(this Interest interest)
        {
            var result = new InterestOverviewViewModel
            {
                Id = interest.Id,
                Animal = interest.Animal,
                AnimalId = interest.AnimalId,
                Customer = interest.Customer,
                CustomerId = interest.CustomerId,
                Comment = interest.Comment,
                AddedOn = interest.AddedOn

            };
            return result;
        }


        public static List<AnimalsForAdoptionViewModel> ToViewModel(this IEnumerable<AnimalForAdoption> Animals)
        {
            var result = new List<AnimalsForAdoptionViewModel>();

            foreach (var animal in Animals)
            {
                result.Add(animal.ToViewModel());
            }

            return result;
        }

        public static AnimalsForAdoptionViewModel ToViewModel(this AnimalForAdoption animal)
        {
            var result = new AnimalsForAdoptionViewModel
            {
                Id = animal.Id,

                Customer = animal.Customer,
                CustomerId = animal.CustomerId,
                AddedOn = animal.AddedOn,
                ReasonAdoptable = animal.ReasonAdoptable,
                Gender = animal.Gender,
                Name = animal.Name,
                Sterilised = animal.Sterilised,
                Type = animal.Type

            };
            return result;
        }
    }
}

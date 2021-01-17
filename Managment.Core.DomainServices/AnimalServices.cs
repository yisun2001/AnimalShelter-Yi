using Managment.Core.Domain;
using Managment.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public class AnimalServices : IAnimalServices
    {

        private readonly IAnimalRepository _animalRepository;
        private readonly IResidenceRepository _residenceRepository;

        public AnimalServices(
            IAnimalRepository animalRepository,
            IResidenceRepository residenceRepository
            )
        {
            _animalRepository = animalRepository;
            _residenceRepository = residenceRepository;
        }
        public async Task AddAnimal(Animal animal) {
            var CurrentResidence = _residenceRepository.GetById(animal.ResidenceId);
            int AnimalsInResidence = _animalRepository.GetAnimalsByResidence(animal.ResidenceId).Count();
            ArrivalDateIsLaterThanDateOfBirth(animal);
            DateOfBirthIsNotLaterThanDateOfPassing(animal);
            ArrivalDateIsLaterThanDateOfPassing(animal);
            DateOfBirthAndEstimatedAgeCheck(animal);
            AnimalTypeResidenceAndAnimalTypeCheck(animal, CurrentResidence);
            CapacityResidenceCheck(animal, CurrentResidence, AnimalsInResidence);
            GenderResidenceAndGenderAnimalCheck(animal, CurrentResidence);
            await _animalRepository.AddAnimal(animal);

        }

        public async Task UpdateAnimal(Animal animal)
        {
            var CurrentResidence = _residenceRepository.GetById(animal.ResidenceId);
            int AnimalsInResidence = _animalRepository.GetAnimalsByResidence(animal.ResidenceId).Count();
            ArrivalDateIsLaterThanDateOfBirth(animal);
            DateOfBirthIsNotLaterThanDateOfPassing(animal);
            ArrivalDateIsLaterThanDateOfPassing(animal);
            DateOfBirthAndEstimatedAgeCheck(animal);
            AnimalTypeResidenceAndAnimalTypeCheck(animal, CurrentResidence);
            CapacityResidenceCheck(animal, CurrentResidence, AnimalsInResidence);
            GenderResidenceAndGenderAnimalCheck(animal, CurrentResidence);
            await _animalRepository.UpdateAnimal(animal);

        }

        private void DateOfBirthAndEstimatedAgeCheck(Animal animal)
        {
            if (animal.DateOfBirth != null && animal.EstimatedAge != null)
            {
                throw new InvalidOperationException("Geboortedatum en Geschatte leeftijd mogen niet tegelijkertijd ingevuld zijn");
            }else
            if (animal.DateOfBirth == null && animal.EstimatedAge == null)
            {
                throw new InvalidOperationException("Geboortedatum of Geschatte leeftijd moet ingevuld zijn");
            }

        }

        private void AnimalTypeResidenceAndAnimalTypeCheck(Animal animal, Residence CurrentResidence) 
        {
            var ResidenceAnimalType = CurrentResidence.Type;
            if (animal.Type != ResidenceAnimalType)
            {
                throw new InvalidOperationException("Dier Type is niet gelijk");
            }
        }

        private void CapacityResidenceCheck(Animal animal, Residence currentResidence, int AnimalsInResidence)
        {
            var MaxCapacity = currentResidence.Capacity;

            if (AnimalsInResidence == MaxCapacity)
            {
                throw new InvalidOperationException("Verblijf zit al vol, Selecteer een ander verblijf");
            }
        }

        private void GenderResidenceAndGenderAnimalCheck(Animal animal, Residence CurrentResidence)
        {
            int MaxCapacity = CurrentResidence.Capacity;
            var GenderResidence = CurrentResidence.Gender;
            if (MaxCapacity > 1 && animal.Gender != GenderResidence && animal.Sterilised == false)
            {
               throw new InvalidOperationException("Residence Gender komt niet overeen met Animal Gender");
            }
        }

        private void ArrivalDateIsLaterThanDateOfBirth(Animal animal) {
            if (animal.DateOfBirth != null) {
            if (animal.DateOfArrival.CompareTo(animal.DateOfBirth) < 0 )
            {
                throw new InvalidOperationException("Geboortedatum mag niet later zijn dan Datum binnenkomst van dier");
            }
            }
            
        }

        private void ArrivalDateIsLaterThanDateOfPassing(Animal animal) {
            if (animal.DateOfPassing != null)
            {
                if (animal.DateOfArrival.CompareTo(animal.DateOfPassing) > 0)
                {
                    throw new InvalidOperationException("Datum van overlijden mag niet eerder zijn dan Datum binnenkomst van dier");
                }
            }

        }

        private void DateOfBirthIsNotLaterThanDateOfPassing(Animal animal)
        {
            if (animal.DateOfPassing != null && animal.DateOfBirth != null)
            {
                if (animal.DateOfBirth?.CompareTo(animal.DateOfPassing) > 0)
                {
                    throw new InvalidOperationException("Datum van overlijden mag niet later zijn dan geboortedatum van dier");
                }
            }

        }
    }
}

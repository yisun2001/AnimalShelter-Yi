using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public class TreatmentServices : ITreatmentServices
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentServices(
            IAnimalRepository animalRepository,
            ITreatmentRepository treatmentRepository
            )
        {
            _animalRepository = animalRepository;
            _treatmentRepository = treatmentRepository;
        }
        public async Task AddTreatment(Treatment treatment)
        {
            if (treatment.Type == TreatmentType.Sterilisatie || treatment.Type == TreatmentType.Castratie)
            {
                treatment.AllowedAgeInMonths = 6;
            }
            var animal = _animalRepository.GetById(treatment.AnimalId);
            treatment.Animal = animal;
            ArrivalDateIsLaterThanPerformedOn(treatment, animal);
            DateOfPassingIsLaterThanPerformedOn(treatment, animal);
            DateOfAdoptionIsLaterThanPerformedOn(treatment, animal);
            PerformedIsLaterThanDateOfAdoption(treatment, animal);
            TreatmentTypeCommentCheck(treatment);
            SterolizedOrCastratedAgeLimitCheck(treatment, animal);
            SterolizedOrCastratedCheck(treatment, animal);
            EuthenasieCheckAndSetAnimalPassing(treatment, animal);
            await _treatmentRepository.AddTreatment(treatment);
        }

        public async Task UpdateTreatment(Treatment treatment)
        {
            if (treatment.Type == TreatmentType.Sterilisatie || treatment.Type == TreatmentType.Castratie)
            {
                treatment.AllowedAgeInMonths = 6;
            }
            var animal = _animalRepository.GetById(treatment.AnimalId);
            treatment.Animal = animal;
            ArrivalDateIsLaterThanPerformedOn(treatment, animal);
            DateOfPassingIsLaterThanPerformedOn(treatment, animal);
            DateOfAdoptionIsLaterThanPerformedOn(treatment, animal);
            PerformedIsLaterThanDateOfAdoption(treatment, animal);
            TreatmentTypeCommentCheck(treatment);
            SterolizedOrCastratedAgeLimitCheck(treatment, animal);
            SterolizedOrCastratedCheck(treatment, animal);
            EuthenasieCheckAndSetAnimalPassing(treatment, animal);
            await _treatmentRepository.UpdateTreatment(treatment);

        }

        private void SterolizedOrCastratedAgeLimitCheck(Treatment treatment, Animal animal) {
            if (treatment.Type == TreatmentType.Sterilisatie || treatment.Type == TreatmentType.Castratie)
            {
                if (treatment.PerformedOn.Year - animal.DateOfBirth.Value.Year < 1 && treatment.PerformedOn.Month - animal.DateOfBirth.Value.Month < treatment.AllowedAgeInMonths.Value)
                {
                    throw new InvalidOperationException("Het dier mag niet gecasteerd of gesteraliseerd worden wegens leeftijd van het dier");
                }
            }
        }
        private void SterolizedOrCastratedCheck(Treatment treatment, Animal animal) {
            if (animal.Sterilised == true && (treatment.Type == TreatmentType.Sterilisatie || treatment.Type == TreatmentType.Castratie))
            {
                throw new InvalidOperationException("Het dier is al gecasteerd of gesteraliseerd");
            }
            SterolizedOrCastratedAndSetAnimalSterisedTrue(treatment, animal);

        }
        private void SterolizedOrCastratedAndSetAnimalSterisedTrue(Treatment treatment, Animal animalToUpdate) {
            if (treatment.Type == TreatmentType.Sterilisatie || treatment.Type == TreatmentType.Castratie)
            {
                treatment.Animal.Sterilised = true;

                //animalToUpdate.Sterilised = true;
                //_animalRepository.UpdateAnimal(animalToUpdate);
            }
        }

        private void TreatmentTypeCommentCheck(Treatment treatment) {

            if (treatment.Type == TreatmentType.Chippen && treatment.Description == null) {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Chippen");
            }else
            if (treatment.Type == TreatmentType.Euthanasie && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Euthenasie");
            }
            else
                if (treatment.Type == TreatmentType.Inenting && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Inenting");
            }
            else
                if (treatment.Type == TreatmentType.Operatie && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Operatie");
            }
            /*
                TreatmentType treatmentType;
            if (Enum.TryParse(treatment.Type.ToString(), out treatmentType) && treatmentType == TreatmentType.Chippen && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Chippen");
            }else
            if (Enum.TryParse(treatment.Type.ToString(), out treatmentType) && treatmentType == TreatmentType.Euthanasie && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Euthanasie");
            }
            else
            if (Enum.TryParse(treatment.Type.ToString(), out treatmentType) && treatmentType == TreatmentType.Inenting && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Inenting");
            }
            else
            if (Enum.TryParse(treatment.Type.ToString(), out treatmentType) && treatmentType == TreatmentType.Operatie && treatment.Description == null)
            {
                throw new InvalidOperationException("Opmerking moet ingevult worden bij Operatie");
            }
            */
        }

        private void ArrivalDateIsLaterThanPerformedOn(Treatment treatment, Animal animal)
        {
                if (animal.DateOfArrival.CompareTo(treatment.PerformedOn) > 0)
                {
                    throw new InvalidOperationException("Datum Binnenkomst mag niet later zijn dan datum van behandeling van dier");
                }
        }

        private void PerformedIsLaterThanDateOfAdoption(Treatment treatment, Animal animal) {

            if (animal.DateOfAdoption?.CompareTo(treatment.PerformedOn) < 0) {
                throw new InvalidOperationException("Datum vanbehandeling mag niet later zijn dan datum van adoptie van dier");
            }
        }

        private void DateOfPassingIsLaterThanPerformedOn(Treatment treatment, Animal animal)
        {
            if (animal.DateOfPassing?.CompareTo(treatment.PerformedOn) < 0)
            {
                throw new InvalidOperationException("Datum behandeling mag niet later zijn dan datum overlijden van dier");
            }
        }

        private void DateOfAdoptionIsLaterThanPerformedOn(Treatment treatment, Animal animal)
        {
            if (animal.DateOfAdoption?.CompareTo(treatment.PerformedOn) < 0)
            {
                throw new InvalidOperationException("Datum behandeling mag niet later zijn dan datum adoptie van dier");
            }
        }

        private void EuthenasieCheckAndSetAnimalPassing(Treatment treatment, Animal animalToUpdate) {
            if (animalToUpdate.DateOfPassing.HasValue && treatment.Type == TreatmentType.Euthanasie) {
                throw new InvalidOperationException("Het dier is al dood");
            }
            if (treatment.Type == TreatmentType.Euthanasie) {
                treatment.Animal.DateOfPassing = treatment.PerformedOn;
                treatment.Animal.Adoptable = false;
            }
        }
    }
}

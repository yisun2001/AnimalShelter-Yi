/*using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UI.ASMApp.Models;
using UI.ASMApp.Controllers;
using Core.DomainServices;
using EF.Infrastructure;
using System.Runtime.InteropServices;
using Core.Domain;

namespace UI.ASMApp.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void Specific_TreatmentType_Should_Require_Description()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var treatmentRepoMock = new Mock<ITreatmentRepository>();
            var animalRepoMock = new Mock<IAnimalRepository>();
            Animal animal = new Animal();
            Treatment treatment = new Treatment();
            animal.AddTreatment(treatment);
            AddTreatmentViewModel model = new AddTreatmentViewModel();
*/
           /*
            switch (model.TypeOfTreatment)
            {
                case TypeOfTreatment.Vaccination:
                    ModelState.AddModelError(nameof(model.Description), "Bij deze behandeling is een omschrijving verplicht.");
                    break;

            });

            // Act
            Exception ex = Assert.Throws<ArgumentNullException>(() => model.Description("wrong"));*//*

            // Assert
            Assert.Equal("Bij deze behandeling is een omschrijving verplicht.", ex.Message);*/
/*
        }
    }
}
*/
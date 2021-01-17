using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Customer.Portal.Models;
using Managment.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Customer.Portal.Controllers
{
    public class AnimalForAdoptionController : Controller
    {
        [Authorize(policy: "CustomerOnly")]
        [HttpGet]
        public IActionResult AddAnimal()
        {
            var model = new AnimalForAdoptionViewModel();
            PrefillSelectOptions();
            return View(model);
        }
        private void PrefillSelectOptions()
        {
            var types = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>();
            ViewBag.Types = new SelectList(types, "Type");

            var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            ViewBag.Genders = new SelectList(genders, "Gender");

            var kidFriendly = Enum.GetValues(typeof(KidFriendly));
            ViewBag.KidFriendly = new SelectList(kidFriendly, "KidFriendly");
        }
        [Authorize(policy: "CustomerOnly")]
        [HttpPost]
        public async Task<IActionResult> AddAnimal(AnimalForAdoptionViewModel animalForAdoptionViewModel) {
            var email = User.Identity.Name;
            List<CustomerViewModel> currentCustomer = new List<CustomerViewModel>();
            List<Residence> residences = new List<Residence>();
            List<Animal> animalsList = new List<Animal>();

            AnimalForAdoption animal = new AnimalForAdoption
            {
                Sterilised = animalForAdoptionViewModel.Sterilised,
                ReasonAdoptable = animalForAdoptionViewModel.ReasonAdoptable,
                Gender = animalForAdoptionViewModel.Gender,
                Name = animalForAdoptionViewModel.Name,
                Type = animalForAdoptionViewModel.Type
            };
            if (ModelState.IsValid) {
                using (var httpClient = new HttpClient()) {
                    using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/customers?email=" + email))
                    {
                        string apiresponse = await response.Content.ReadAsStringAsync();
                        currentCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(apiresponse);
                    }


                    animal.CustomerId = currentCustomer.First().Id;


                    using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/residences?" + $"Type={animal.Type}" ))
                    {
                        string apiresponse = await response.Content.ReadAsStringAsync();
                        residences = JsonConvert.DeserializeObject<List<Residence>>(apiresponse);
                    }
                    using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/animals?adoptable=false"))
                    {
                        string apiresponse = await response.Content.ReadAsStringAsync();
                        animalsList = JsonConvert.DeserializeObject<List<Animal>>(apiresponse);
                    }
                    foreach (var residence in residences) {
                        List<Animal> animalsInResidence = new List<Animal>(
                            animalsList.Where(animal => animal.ResidenceId == residence.Id)
                            );
                        foreach (var animalres in animalsInResidence) {
                            if (animalres.Sterilised != true && residence.Gender != animalres.Gender) {
                                residences.Remove(residence);
                                break;
                            }
                            else if (residence.Gender != animal.Gender && animal.Sterilised == true && animalres.Sterilised == true) {
                                break;
                            }
                        }
                        if (animalsInResidence.Count() == residence.Capacity) {
                            residences.Remove(residence);
                            break;
                        }
                        
                    }
                    if (residences.Count() > 0)
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(animal), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("https://localhost:44319/api/v1/animalsforadoption", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            animal = JsonConvert.DeserializeObject<AnimalForAdoption>(apiResponse);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Overview", "Animal");
                            };
                        }
                    }
                    else {
                        ModelState.AddModelError(animal.Name, "Alle verblijven bij dit asiel zitten val voor dit type en geslacht dier");
                    }
                    
                }
            }

            
            return View();

        }
    }
}

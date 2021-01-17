using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Customer.Portal.Models;
using Managment.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Customer.Portal.Controllers
{

    public class InterestController : Controller
    {
        private SignInManager<IdentityUser> signInManager;

        public InterestController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Authorize(policy: "CustomerOnly")]
        public async Task<IActionResult> InterestOverview()
        {
            var email = User.Identity.Name;

            List<CustomerViewModel> currentCustomer = new List<CustomerViewModel>();
            List<InterestViewModel> interestList = new List<InterestViewModel>();
            Animal animal = new Animal();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/customers?email=" + email)) {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    currentCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(apiresponse);
                }
                if (currentCustomer != null) {
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/interests?customerId=" + currentCustomer.First().Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    interestList = JsonConvert.DeserializeObject<List<InterestViewModel>>(apiResponse);
                }
                    foreach (var interest in interestList) {
                        using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/animals/" + interest.AnimalId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
                            interest.Animal = animal;
                    }
                    }
                
                }
                
            }

            return View(interestList);
        }

        [Authorize(policy: "CustomerOnly")]
        [HttpGet]
        public IActionResult AddInterest()
        {
            var model = new InterestViewModel();

            return View(model);
        }

        [Authorize(policy: "CustomerOnly")]
        [HttpPost]
        public async Task<IActionResult> AddInterest(InterestViewModel interestViewModel, int id)
        {
            var email = User.Identity.Name;
            List<CustomerViewModel> currentCustomer = new List<CustomerViewModel>();
            List<InterestViewModel> interestList = new List<InterestViewModel>();
            


            if (ModelState.IsValid) {
             using (var httpClient = new HttpClient())
             {
                
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/customers?email=" + email))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    currentCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(apiresponse);
                }
                
                    Interest interest = new Interest
                    {
                        AnimalId = id,
                        Comment = interestViewModel.Comment,
                        CustomerId = currentCustomer.First().Id
                    };
                   
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/interests?customerId=" + currentCustomer.First().Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    interestList = JsonConvert.DeserializeObject<List<InterestViewModel>>(apiResponse);
                }
                if (interestList.Count() >= 3) {
                        ModelState.AddModelError(interest.Comment, "U kunt geen interesses meer toevoegen. U heeft er al 3");
                }
                else {
                StringContent content = new StringContent(JsonConvert.SerializeObject(interest), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44319/api/v1/interests", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    interest = JsonConvert.DeserializeObject<Interest>(apiResponse);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Details", "Animal", new { Id = id });
                            };
                        }
                } 
                    
             }
                
                
            }
           return View();
        }


        [Authorize(policy: "CustomerOnly")]
        public async Task<IActionResult> DeleteInterest(int id) {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44319/api/v1/interests/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("InterestOverview");

        }
    }
}

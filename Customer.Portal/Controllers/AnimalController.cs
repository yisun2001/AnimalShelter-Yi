using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Customer.Portal.Models;
using Managment.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customer.Portal.Controllers
{
    public class AnimalController : Controller
    {
        
        public async Task<IActionResult> Overview(string type, string gender, string kidfriendly)
        {
            List<AnimalOverviewViewModel> animalList = new List<AnimalOverviewViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    "https://localhost:44319/api/v1/animals?" + (type!=null? $"type={type}" : "") + "&" + 
                                                               (gender != null ? $"gender={gender}" : "") + "&"+
                                                               (kidfriendly != null ? $"kidfriendly={kidfriendly}" : "")))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animalList = JsonConvert.DeserializeObject<List<AnimalOverviewViewModel>>(apiResponse);
                }
            }
            
                return View(animalList);
        }

        public async Task<IActionResult> Details(int id) {
            AnimalOverviewViewModel animal = new AnimalOverviewViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44319/api/v1/animals/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animal = JsonConvert.DeserializeObject<AnimalOverviewViewModel>(apiResponse);
                }
            }
            return View(animal);
        }
    }
}

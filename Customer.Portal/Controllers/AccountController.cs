using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Customer.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customer.Portal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
           

            //IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await userManager.FindByNameAsync(loginModel.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser(registerViewModel.Email);

                await userManager.CreateAsync(user, registerViewModel.Password);
                var result = await userManager.AddClaimAsync(user, new Claim("Customer", "true"));
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    Managment.Core.Domain.Customer customerToCreate = new Managment.Core.Domain.Customer
                    {
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        Email = registerViewModel.Email,
                        DateOfBirth = registerViewModel.DateOfBirth,
                        City = registerViewModel.City,
                        HouseNumber = registerViewModel.HouseNumber,
                        PostCode = registerViewModel.PostCode,
                        Street = registerViewModel.Street,
                        TelephoneNumber = registerViewModel.TelephoneNumber
                    };
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(customerToCreate), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:44319/api/v1/customers", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            customerToCreate = JsonConvert.DeserializeObject<Managment.Core.Domain.Customer>(apiResponse);
                            if (response.IsSuccessStatusCode) {
                                return RedirectToAction("Overview", "Animal");
                            };
                        }
                        
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }

    }
}

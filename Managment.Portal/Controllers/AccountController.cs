using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Managment.Portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Managment.Portal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private readonly IVolunteerRepository _volunteerRepository;

        public AccountController(UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr, IVolunteerRepository volunteerRepository)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            _volunteerRepository = volunteerRepository;

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
        public IActionResult Register() {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl) {
            if (ModelState.IsValid) { 
            IdentityUser user = new IdentityUser(registerViewModel.Email);
            
            await userManager.CreateAsync(user, registerViewModel.Password);
            var result = await userManager.AddClaimAsync(user, new Claim("Volunteer", "true"));
                if (result.Succeeded) {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    Volunteer volunteerToCreate = new Volunteer {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    DateOfBirth = registerViewModel.DateOfBirth,
                    TelephoneNumber = registerViewModel.TelephoneNumber
                    };
                    await _volunteerRepository.AddVolunteer(volunteerToCreate);
                    return RedirectToAction("Overview", "Animal");
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }


    }
}

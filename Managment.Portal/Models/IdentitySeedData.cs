using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Managment.Portal.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        private const string playerUser = "Player";
        private const string playerPassword = "Secret123$";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser vrijwilliger = await userManager.FindByIdAsync(adminUser);
            if (vrijwilliger == null)
            {
                vrijwilliger = new IdentityUser("Admin");
                await userManager.CreateAsync(vrijwilliger, adminPassword);
                await userManager.AddClaimAsync(vrijwilliger, new Claim("Vrijwilliger", "true"));
            }

            IdentityUser klant = await userManager.FindByIdAsync(playerUser);
            if (klant == null)
            {
                klant = new IdentityUser("Player");
                await userManager.CreateAsync(klant, playerPassword);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.DomainServices
{
    public static class ClaimsStore
    {

        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Role", "Volunteer"),
            new Claim("Role", "Client"),
            new Claim("Role", "Admin")
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Module.Identity.Cookie
{
    public static class CookieLogin
    {
        public static void SignIn(this IAuthenticationManager manager, string userId, string name, string displayName, string authenticationType = "ApplicationCookie")
        {
            manager.SignIn(new AuthenticationProperties { IsPersistent = false }, new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypesConst.DisplayName,displayName),
            }, authenticationType));
        }

        //todo get DisplayName


    }


    public static class ClaimTypesConst
    {
        public const string DisplayName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/displayname";
    }
}

using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Microsoft.Owin.Security
{
    /// <summary>
    /// Identity Cookie
    /// </summary>
    public static class CookieSecurity
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="authenticationType"></param>
        public static void SignIn(this IAuthenticationManager manager, string userId, string name, string displayName = null, string authenticationType = "ApplicationCookie")
        {
            if (string.IsNullOrEmpty(displayName))
                displayName = name;
            manager.SignIn(new AuthenticationProperties { IsPersistent = false }, new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypesConst.DisplayName,displayName),
            }, authenticationType));
        }
    }

    public static class ClaimTypesConst
    {
        public const string DisplayName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/displayname";
    }
}

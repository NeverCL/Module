using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Module.Identity.Cookie;

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

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetUserInfo()
        {
            var user = GetUser();
            if (user == null)
                return null;
            return new UserInfo
            {
                Id = user.GetUserId(),
                Name = user.GetUserName(),
                DisplayName = user.GetDisplayName()
            };
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static ClaimsIdentity GetUser()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
                return null;
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;
            return claimsIdentity;
        }
    }

    public static class ClaimTypesConst
    {
        public const string DisplayName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/displayname";
    }
}

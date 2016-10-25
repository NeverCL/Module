using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using Module.Identity.Cookie;

namespace Microsoft.AspNet.Identity
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// 获取昵称
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetDisplayName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
                return claimsIdentity.FindFirst(ClaimTypesConst.DisplayName).Value;
            return null;
        }

        /// <summary>
        /// 获取昵称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetDisplayName<T>(this IIdentity identity) where T : IConvertible
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                string firstValue = claimsIdentity.FindFirst(ClaimTypesConst.DisplayName).Value;
                if (firstValue != null)
                    return (T)Convert.ChangeType((object)firstValue, typeof(T), (IFormatProvider)CultureInfo.InvariantCulture);
            }
            return default(T);
        }

    }
}

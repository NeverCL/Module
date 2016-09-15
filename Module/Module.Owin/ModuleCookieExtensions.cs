using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Module.Owin
{
    public static class ModuleCookieExtensions
    {
        public static IAppBuilder UseModuleCookie(this IAppBuilder app, string url = "/Account/Login")
        {
            if (app == null)
                throw new ArgumentNullException("app is null");
            return app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(url)
            });
        }
    }
}

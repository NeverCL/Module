﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;

namespace Module.WebApi
{
    public class SsoController : ApiController
    {
        [HttpGet]
        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        [HttpGet]
        public string GetUserName()
        {
            return User.Identity.Name;
        }

        [AllowAnonymous]
        [HttpGet]
        public RedirectResult Login(string token, string returnUrl)
        {
            var client = new HttpClient();
            var url = GetCfgValue("ssoUrl");
            string clientId = GetCfgValue("clientId"), clientSecret = GetCfgValue("clientSecret");
            var rst = client.PostAsync(url + "api/user/GetUserInfo", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"token",token },
                {"clientId", clientId},
                {"clientSecret", clientSecret}
            })).Result.Content.ReadAsStringAsync().Result;
            ClaimsIdentity identity = CreateIdentity(rst);
            Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return Redirect(new Uri(returnUrl, UriKind.RelativeOrAbsolute));
        }

        ClaimsIdentity CreateIdentity(string rst)
        {
            return new ClaimsIdentity(new[]
                        {
                new Claim(ClaimTypes.NameIdentifier, JObject.Parse(rst)["Id"].Value<string>()),
                new Claim(ClaimTypes.Name, JObject.Parse(rst)["UserName"].Value<string>()),
                new Claim(ClaimTypes.GivenName, JObject.Parse(rst)["DisplayName"].Value<string>())
            }, DefaultAuthenticationTypes.ApplicationCookie);
        }

        string GetCfgValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}
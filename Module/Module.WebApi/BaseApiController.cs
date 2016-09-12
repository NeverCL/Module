using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using NLog;

namespace Module.WebApi
{
    [NotImplExceptionFilter]
    public abstract class BaseApiController : ApiController
    {
        protected ILogger Logger { get; set; }

        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        public string GetUserName()
        {
            return User.Identity.Name;
        }

        [AllowAnonymous]
        public void Login(LoginInfo loginInfo)
        {
            var client = new HttpClient();
            var url = GetCfgValue("ssoUrl");
            string clientId = GetCfgValue("clientId"), clientSecret = GetCfgValue("clientSecret");
            var rst = client.PostAsync(url + "api/user/GetUserInfo", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"token",loginInfo.Token },
                {"clientId", clientId},
                {"clientSecret", clientSecret}
            })).Result.Content.ReadAsStringAsync().Result;
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, JObject.Parse(rst)["Id"].Value<string>()),
                new Claim(ClaimTypes.Name, JObject.Parse(rst)["UserName"].Value<string>()),
                new Claim(ClaimTypes.GivenName, JObject.Parse(rst)["DisplayName"].Value<string>())
            }, DefaultAuthenticationTypes.ApplicationCookie);
            Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            Redirect(loginInfo.ReturnUrl);
        }

        protected virtual string GetCfgValue(string name)
        {
            return name;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }

    public class LoginInfo
    {
        public string Token { get; set; }

        public string ReturnUrl { get; set; }
    }
}

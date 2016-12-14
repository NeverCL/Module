using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using NLog;
using System.Configuration;
using System.Threading;

namespace Module.WebApi
{
    [ProcessExceptionFilter]
    public abstract class BaseApiController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected ClaimsIdentity GetUser()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
                return null;
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;
            return claimsIdentity;
        }

        protected string GetUserId()
        {
            return GetUser().GetUserId();
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }

}

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

namespace Module.WebApi
{
    public abstract class BaseApiController : ApiController
    {
        protected ILogger Logger { get; set; }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }

}

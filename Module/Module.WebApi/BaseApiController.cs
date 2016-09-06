using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;
using NLog;

namespace Module.WebApi
{
    [NotImplExceptionFilter]
    public abstract class BaseApiController : ApiController
    {
        protected ILogger Logger { get; set; }

        protected string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        protected string GetUserName()
        {
            return User.Identity.Name;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello World";
        }
    }
}

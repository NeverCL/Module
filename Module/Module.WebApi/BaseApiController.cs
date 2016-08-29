using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;

namespace Module.WebApi
{
    [NotImplExceptionFilter]
    public abstract class BaseApiController : ApiController
    {
        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        public string GetUserName()
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

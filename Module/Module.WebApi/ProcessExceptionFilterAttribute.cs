using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using NLog;
using System.Net.Http;
using System.Net;

namespace Module.WebApi
{
    public class ProcessExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //The Response Message Set by the Action During Ececution
            var res = context.Exception.Message;
            //Define the Response Message
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(res)
            };
            //Create the Error Response
            context.Response = response;
            LogManager.GetLogger(context.ActionContext.ControllerContext.Controller.GetType().Name).Error(context.Exception);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using LogLayer;

namespace UtilityLayer.Filters
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1. Loglama
            LogServices.AddLog(
                "Kullanıcı Adı",
                "ErrorFormat",
                actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName + " - " +
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace,
                actionExecutedContext.Exception);

            //2. Response Hazırlama
            ExceptionResponse result = new ExceptionResponse();
            result.ErrorAction = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            result.ErrorController = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            result.ErrorMessage = actionExecutedContext.Exception.ToString();

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
    }
}

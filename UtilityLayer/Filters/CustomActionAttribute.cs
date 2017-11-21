using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using LogLayer;
using UtilityLayer.Tools;
using System.Net;

namespace UtilityLayer.Filters
{
    public class CustomActionAttribute : ActionFilterAttribute
    {
        //Action Çalışmadan önceki durum
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
        }
        //Çalıştıktan sonraki durum
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            LogServices.AddLog(
                "Kullanıcı Adı",
                IpGenerator.GetIpAddress(),
                "InfoFormat",
                actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName + " - " +
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                (actionExecutedContext.Response.Content as ObjectContent)?.ObjectType.FullName,
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName +" işlemi yapıldı.",
                null);            
        }
    }
}

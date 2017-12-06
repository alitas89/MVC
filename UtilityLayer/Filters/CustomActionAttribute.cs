using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
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
         
        }
    }
}

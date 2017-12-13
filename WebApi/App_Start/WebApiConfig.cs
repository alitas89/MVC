using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using WebApi.MessageHandlers;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Komple cors uygulaması
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*"); //params=> origins, headers, methods
            //*, *, * olsaydı tüm herşeye izin vermiş olurduk.
            //İlk hane izin verilen web siteleridir => http://localhost:3583, http://localhost:1231 her bir site bu şekilde virgülle ayrılarak //eklenmelidir.
            config.EnableCors(cors);


            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[] {new ApiResponseHandler()}
                    )
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}

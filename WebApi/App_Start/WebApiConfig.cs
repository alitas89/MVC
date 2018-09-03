using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using WebApi.MediaTypes;
using WebApi.MessageHandlers;
using WebApiContrib.Formatting;
using WebApiContrib.Formatting.Xlsx;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Cors izinleri startupta verildi!
            //Komple cors uygulaması
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*"); //params=> origins, headers, methods
            //*, *, * olsaydı tüm herşeye izin vermiş olurduk.
            //İlk hane izin verilen web siteleridir => http://localhost:3583, http://localhost:1231 her bir site bu şekilde virgülle ayrılarak //eklenmelidir.
            //config.EnableCors(cors);


            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            //new QueryStringMapping("format", "csv", "text/csv")
            config.Formatters.Add(new CsvFormat(new QueryStringMapping("format", "csv", "text/csv")));

            config.Formatters.Add(new XlsxFormat(new QueryStringMapping("format", "xlsx", "text/xlsx")));

            config.Formatters.Add(new PdfFormat(new QueryStringMapping("format", "pdf", "application/pdf")));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[] { new ApiResponseHandler() }
                    )
            );

            //Filtrelemeye ve belirli büyüklükte parçalara göre export yapmaya yarayan routing
            config.Routes.MapHttpRoute(
                name: "ExportApi",
                routeTemplate: "api/{controller}/{offset}/{limit}/export/{format}",
                defaults: new
                {
                    offset = RouteParameter.Optional,
                    limit = RouteParameter.Optional,
                    format = RouteParameter.Optional
                }
            );

            
            config.Routes.MapHttpRoute(
               name: "ExportApiVarlikGrup",
               routeTemplate: "api/{controller}/GetByVarlikGrupID/{VarlikGrupID}/{offset}/{limit}/export/{format}",
               defaults: new
               {
                   VarlikGrupID = RouteParameter.Optional,
                   offset = RouteParameter.Optional,
                   limit = RouteParameter.Optional,
                   format = RouteParameter.Optional
               }
           );

            //Normal excel indirme
            //config.Routes.MapHttpRoute(
            //    name: "IdWithExt",
            //    routeTemplate: "api/{controller}/downloadsablon/{format}"
            //);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}

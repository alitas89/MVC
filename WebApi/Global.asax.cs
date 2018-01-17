using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApi.MediaTypes;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            //Api url bilgisine ek olarak format belirtlmesiyle ilgili yapı devreye girer ve ona uygun olarak bir çıktı üretir. url? format = csv
            //GlobalConfiguration.Configuration.Formatters.Add(new CsvFormatter(new QueryStringMapping("format", "csv", "text/csv")));
        }
    }
}

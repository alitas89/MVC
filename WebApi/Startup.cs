using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BusinessLayer.DependencyResolvers.Ninject;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;
using WebApi.Bearer;
using WebApiContrib.IoC.Ninject;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());
            //Cors etkinleştirilir
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            Configure(app);

            WebApiConfig.Register(configuration);

            app.UseWebApi(configuration);
        }

        private void Configure(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/getToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1), //1 saat
                AllowInsecureHttp = true,
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

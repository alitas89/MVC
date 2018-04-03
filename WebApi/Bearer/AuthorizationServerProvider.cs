using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Genel;
using BusinessLayer.DependencyResolvers.Ninject;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Genel;
using Microsoft.Owin.Security.OAuth;

namespace WebApi.Bearer
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IKullaniciService kullaniciService = InstanceFactory.GetInstance<IKullaniciService>();
            IYetkiGrupKullaniciService yetkiGrupKullaniciService = InstanceFactory.GetInstance<IYetkiGrupKullaniciService>();
            IYetkiGrupRolService yetkiGrupRol = InstanceFactory.GetInstance<IYetkiGrupRolService>();
            IYetkiRolService yetkiRol = InstanceFactory.GetInstance<IYetkiRolService>();

            try
            {
                Kullanici kullanici = kullaniciService.GetByKullaniciAdiAndSifre(context.UserName, context.Password);

                if (kullanici != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    //Kullanıcı bilgileri eklenir
                    identity.AddClaim(new Claim(ClaimTypes.Name, kullanici.KullaniciAdi));
                    identity.AddClaim(new Claim(ClaimTypes.Actor, kullanici.Ad + " " + kullanici.Soyad));

                    //Giriş yapabilen her kişiye Authorized rolü verilir
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Authorized"));

                    //Gelen kullanıcı üzerinden grup bilgilerine ulaşılır
                    var arrYetkiGrupID = yetkiGrupKullaniciService.GetListByKullaniciId(kullanici.KullaniciId).Select(u => u.YetkiGrupID).Distinct().ToArray();

                    //Her bir grup için bulunan rolIDleri alınır
                    foreach (var yetkiGrupID in arrYetkiGrupID)
                    {
                        var arrYetkiRolID = yetkiGrupRol.GetListByGrupId(yetkiGrupID).Select(x => x.YetkiRolKod).Distinct().ToArray();
                        foreach (var role in arrYetkiRolID)
                        {
                            //Her bir rolün idsi üzerinden adına ulaşılır ve claim edilir
                            if (role != null)
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            }
                        }
                    }

                    //var roleArray =
                    //    rolService.GetRolByKullaniciId(kullanici.KullaniciId).Select(u => u.Ad).ToArray();
                    ////IPrincipal principal = new GenericPrincipal(new GenericIdentity(context.UserName), rolArray);
                    ////Thread.CurrentPrincipal = principal;
                    ////HttpContext.Current.User = principal;

                    //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    //foreach (var role in roleArray)
                    //{
                    //    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    //}
                    context.Validated(identity);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.Properties.ExpiresUtc = DateTime.Now.AddDays(7);
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BusinessLayer.Abstract;
using BusinessLayer.DependencyResolvers.Ninject;
using EntityLayer.Concrete;
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
            IRolService rolService = InstanceFactory.GetInstance<IRolService>();

            try
            {
                Kullanici kullanici = kullaniciService.GetByKullaniciAdiAndSifre(context.UserName, context.Password);
                if (kullanici != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    var roleArray =
                        rolService.GetRolByKullaniciId(kullanici.KullaniciId).Select(u => u.Ad).ToArray();
                    //IPrincipal principal = new GenericPrincipal(new GenericIdentity(context.UserName), rolArray);
                    //Thread.CurrentPrincipal = principal;
                    //HttpContext.Current.User = principal;

                    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    foreach (var role in roleArray)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                    context.Validated(identity);
                }
            }
            catch 
            {

            }
        }
    }
}
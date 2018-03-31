using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class InfoController : ApiController
    {
        [Route("api/info/kullaniciadi")]
        [HttpGet]
        public string KullaniciAdi()
        {
            return ClaimsPrincipal.Current.Identity.Name;
        }

        [Route("api/info/kullaniciadsoyad")]
        [HttpGet]
        public string KullaniciAdSoyad()
        {
            var actor = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/actor")?.Value;
            return actor;
        }

        [Route("api/info/roles")]
        [HttpGet]
        public string Roles()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            List<string> listRole = new List<string>();
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var claim in claims)
            {
                string name = claim.Type.Substring(claim.Type.LastIndexOf('/'),
                    claim.Type.Length - claim.Type.LastIndexOf('/'));
                if (name=="/role")
                {
                    listRole.Add(claim.Value);
                }
            }
            return JsonConvert.SerializeObject(listRole);
        }
    }
}

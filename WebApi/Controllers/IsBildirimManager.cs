using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;

namespace WebApi.Controllers
{
    public class IsBildirimManager : ApiController
    {
        IIsBildirimService _isBildirimService;

        public IsBildirimManager(IIsBildirimService isBildirimService)
        {
            _isBildirimService = isBildirimService;
        }

        [Route("api/isbildirim/getacikonaysizistalepsayisi")]
        public int GetAcikOnaysizIsTalepSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                   x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                   "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _isBildirimService.GetAcikOnaysizIsTalepSayisi(kullaniciID);
        }

        [Route("api/isbildirim/getacikisemrisayisi")]
        public int GetAcikIsEmriSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _isBildirimService.GetAcikIsEmriSayisi(kullaniciID);
        }

        [Route("api/isbildirim/getsorumluolunanisemrisayisi")]
        public int GetSorumluOlunanIsEmriSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _isBildirimService.GetSorumluOlunanIsEmriSayisi(kullaniciID);
        }

        [Route("api/isbildirim/getisemribakimsonucbildirim")]
        public List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _isBildirimService.GetIsEmriBakimSonucBildirim(kullaniciID);
        }

        [Route("api/isbildirim/getistalepsonucbildirim")]
        public List<IsTalepSonucBildirimTemp> GetIsTalepSonucBildirim()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _isBildirimService.GetIsTalepSonucBildirim(kullaniciID);
        }
    }
}
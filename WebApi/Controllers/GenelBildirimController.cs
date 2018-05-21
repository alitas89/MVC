﻿using System;
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
    public class GenelBildirimController : ApiController
    {
        IGenelBildirimService _genelBildirimService;

        public GenelBildirimController(IGenelBildirimService genelBildirimService)
        {
            _genelBildirimService = genelBildirimService;
        }

        [Route("api/genelbildirim/getacikonaysizistalepsayisi")]
        public int GetAcikOnaysizIsTalepSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                   x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                   "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _genelBildirimService.GetAcikOnaysizIsTalepSayisi(kullaniciID);
        }

        [Route("api/genelbildirim/getacikisemrisayisi")]
        public int GetAcikIsEmriSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _genelBildirimService.GetAcikIsEmriSayisi(kullaniciID);
        }

        [Route("api/genelbildirim/getsorumluolunanisemrisayisi")]
        public int GetSorumluOlunanIsEmriSayisi()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _genelBildirimService.GetSorumluOlunanIsEmriSayisi(kullaniciID);
        }

        [Route("api/genelbildirim/getisemribakimsonucbildirim")]
        public List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim()
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                    x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) ==
                    "/nameidentifier")
                ?.Value;

            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;
            return _genelBildirimService.GetIsEmriBakimSonucBildirim(kullaniciID);
        }
    }
}
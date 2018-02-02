using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Genel;
using EntityLayer.Concrete.Genel;

namespace WebApi.Controllers
{
    public class VerifyController : ApiController
    {
        IVerifyService _verifyService;

        public VerifyController(IVerifyService verifyService)
        {
            _verifyService = verifyService;
        }

        /// <summary>
        /// Bu metod ile token doğrulaması yapılır.
        /// Business içerisindeki SecuredOperation vasıtasıyla tüm roller yazılır.
        /// Herhangi bir role sahip olan kullanıcı yetkilendirilmiştir.
        /// </summary>
        /// <returns></returns>
        public List<Verify> Get()
        {
            return _verifyService.GetList();
        }
    }
}
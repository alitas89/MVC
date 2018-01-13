using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class BilgilendirmeTuruController : ApiController
    {
        IBilgilendirmeTuruService _bilgilendirmeTuruService;

        public BilgilendirmeTuruController(IBilgilendirmeTuruService bilgilendirmeTuruService)
        {
            _bilgilendirmeTuruService = bilgilendirmeTuruService;
        }

        // GET api/<controller>
        public IEnumerable<BilgilendirmeTuru> Get()
        {
            return _bilgilendirmeTuruService.GetList();
        }

        // GET api/<controller>/5
        public BilgilendirmeTuru Get(int id)
        {
            return _bilgilendirmeTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BilgilendirmeTuru bilgilendirmeTuru)
        {
            return _bilgilendirmeTuruService.Add(bilgilendirmeTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BilgilendirmeTuru bilgilendirmeTuru)
        {
            return _bilgilendirmeTuruService.Update(bilgilendirmeTuru);
        }

        public int Delete(int id)
        {
            return _bilgilendirmeTuruService.DeleteSoft(id);
        }

        [Route("api/bilgilendirmeturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bilgilendirmeTuruService.Delete(id);
        }
    }
}
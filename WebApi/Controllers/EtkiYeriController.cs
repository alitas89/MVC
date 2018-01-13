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
    public class EtkiYeriController : ApiController
    {
        IEtkiYeriService _etkiYeriService;

        public EtkiYeriController(IEtkiYeriService etkiYeriService)
        {
            _etkiYeriService = etkiYeriService;
        }

        // GET api/<controller>
        public IEnumerable<EtkiYeri> Get()
        {
            return _etkiYeriService.GetList();
        }

        // GET api/<controller>/5
        public EtkiYeri Get(int id)
        {
            return _etkiYeriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]EtkiYeri etkiYeri)
        {
            return _etkiYeriService.Add(etkiYeri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]EtkiYeri etkiYeri)
        {
            return _etkiYeriService.Update(etkiYeri);
        }

        public int Delete(int id)
        {
            return _etkiYeriService.DeleteSoft(id);
        }

        [Route("api/etkiyeri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _etkiYeriService.Delete(id);
        }
    }
}
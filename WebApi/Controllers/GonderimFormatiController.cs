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
    public class GonderimFormatiController : ApiController
    {
        IGonderimFormatiService _gonderimFormatiService;

        public GonderimFormatiController(IGonderimFormatiService gonderimFormatiService)
        {
            _gonderimFormatiService = gonderimFormatiService;
        }

        // GET api/<controller>
        public IEnumerable<GonderimFormati> Get()
        {
            return _gonderimFormatiService.GetList();
        }

        // GET api/<controller>/5
        public GonderimFormati Get(int id)
        {
            return _gonderimFormatiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Add(gonderimFormati);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Update(gonderimFormati);
        }

        public int Delete(int id)
        {
            return _gonderimFormatiService.DeleteSoft(id);
        }

        [Route("api/gonderimformati/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _gonderimFormatiService.Delete(id);
        }
    }
}
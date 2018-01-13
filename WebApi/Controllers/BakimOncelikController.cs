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
    public class BakimOncelikController : ApiController
    {
        IBakimOncelikService _bakimOncelikService;

        public BakimOncelikController(IBakimOncelikService bakimOncelikService)
        {
            _bakimOncelikService = bakimOncelikService;
        }

        // GET api/<controller>
        public IEnumerable<BakimOncelik> Get()
        {
            return _bakimOncelikService.GetList();
        }

        // GET api/<controller>/5
        public BakimOncelik Get(int id)
        {
            return _bakimOncelikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimOncelik bakimOncelik)
        {
            return _bakimOncelikService.Add(bakimOncelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimOncelik bakimOncelik)
        {
            return _bakimOncelikService.Update(bakimOncelik);
        }

        public int Delete(int id)
        {
            return _bakimOncelikService.DeleteSoft(id);
        }

        [Route("api/bakimoncelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimOncelikService.Delete(id);
        }
    }
}
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
    public class OncelikController : ApiController
    {
        IOncelikService _oncelikService;

        public OncelikController(IOncelikService oncelikService)
        {
            _oncelikService = oncelikService;
        }

        // GET api/<controller>
        public IEnumerable<Oncelik> Get()
        {
            return _oncelikService.GetList();
        }

        // GET api/<controller>/5
        public Oncelik Get(int id)
        {
            return _oncelikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Oncelik oncelik)
        {
            return _oncelikService.Add(oncelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Oncelik oncelik)
        {
            return _oncelikService.Update(oncelik);
        }

        public int Delete(int id)
        {
            return _oncelikService.DeleteSoft(id);
        }

        [Route("api/oncelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _oncelikService.Delete(id);
        }
    }
}
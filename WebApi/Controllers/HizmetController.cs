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
    public class HizmetController : ApiController
    {
        IHizmetService _hizmetService;

        public HizmetController(IHizmetService hizmetService)
        {
            _hizmetService = hizmetService;
        }

        // GET api/<controller>
        public IEnumerable<Hizmet> Get()
        {
            return _hizmetService.GetList();
        }

        // GET api/<controller>/5
        public Hizmet Get(int id)
        {
            return _hizmetService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Add(hizmet);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Update(hizmet);
        }

        public int Delete(int id)
        {
            return _hizmetService.DeleteSoft(id);
        }

        [Route("api/hizmet/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hizmetService.Delete(id);
        }
    }
}
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
    public class ArizaNedeniController : ApiController
    {
        IArizaNedeniService _arizaNedeniService;

        public ArizaNedeniController(IArizaNedeniService arizaNedeniService)
        {
            _arizaNedeniService = arizaNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaNedeni> Get()
        {
            return _arizaNedeniService.GetList();
        }

        // GET api/<controller>/5
        public ArizaNedeni Get(int id)
        {
            return _arizaNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ArizaNedeni arizaNedeni)
        {
            return _arizaNedeniService.Add(arizaNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ArizaNedeni arizaNedeni)
        {
            return _arizaNedeniService.Update(arizaNedeni);
        }

        public int Delete(int id)
        {
            return _arizaNedeniService.DeleteSoft(id);
        }

        [Route("api/arizanedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _arizaNedeniService.Delete(id);
        }
    }
}
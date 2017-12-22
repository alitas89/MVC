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
    public class DurusNedeniController : ApiController
    {
        IDurusNedeniService _durusNedeniService;

        public DurusNedeniController(IDurusNedeniService durusNedeniService)
        {
            _durusNedeniService = durusNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<DurusNedeni> Get()
        {
            return _durusNedeniService.GetList();
        }

        // GET api/<controller>/5
        public DurusNedeni Get(int id)
        {
            return _durusNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]DurusNedeni durusNedeni)
        {
            return _durusNedeniService.Add(durusNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]DurusNedeni durusNedeni)
        {
            return _durusNedeniService.Update(durusNedeni);
        }

        public int Delete(int id)
        {
            return _durusNedeniService.DeleteSoft(id);
        }

        [Route("api/durusnedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _durusNedeniService.Delete(id);
        }
    }
}
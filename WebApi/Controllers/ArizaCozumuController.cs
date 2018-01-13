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
    public class ArizaCozumuController : ApiController
    {
        IArizaCozumuService _arizaCozumuService;

        public ArizaCozumuController(IArizaCozumuService arizaCozumuService)
        {
            _arizaCozumuService = arizaCozumuService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaCozumu> Get()
        {
            return _arizaCozumuService.GetList();
        }

        // GET api/<controller>/5
        public ArizaCozumu Get(int id)
        {
            return _arizaCozumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ArizaCozumu arizaCozumu)
        {
            return _arizaCozumuService.Add(arizaCozumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ArizaCozumu arizaCozumu)
        {
            return _arizaCozumuService.Update(arizaCozumu);
        }

        public int Delete(int id)
        {
            return _arizaCozumuService.DeleteSoft(id);
        }

        [Route("api/arizacozumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _arizaCozumuService.Delete(id);
        }
    }
}
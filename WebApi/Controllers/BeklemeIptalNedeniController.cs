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
    public class BeklemeIptalNedeniController : ApiController
    {
        IBeklemeIptalNedeniService _beklemeIptalNedeniService;

        public BeklemeIptalNedeniController(IBeklemeIptalNedeniService beklemeIptalNedeniService)
        {
            _beklemeIptalNedeniService = beklemeIptalNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<BeklemeIptalNedeni> Get()
        {
            return _beklemeIptalNedeniService.GetList();
        }

        // GET api/<controller>/5
        public BeklemeIptalNedeni Get(int id)
        {
            return _beklemeIptalNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BeklemeIptalNedeni beklemeIptalNedeni)
        {
            return _beklemeIptalNedeniService.Add(beklemeIptalNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BeklemeIptalNedeni beklemeIptalNedeni)
        {
            return _beklemeIptalNedeniService.Update(beklemeIptalNedeni);
        }

        public int Delete(int id)
        {
            return _beklemeIptalNedeniService.DeleteSoft(id);
        }

        [Route("api/beklemeıptalnedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _beklemeIptalNedeniService.Delete(id);
        }
    }
}
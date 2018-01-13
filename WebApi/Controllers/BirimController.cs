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
    public class BirimController : ApiController
    {
        IBirimService _birimService;

        public BirimController(IBirimService birimService)
        {
            _birimService = birimService;
        }

        // GET api/<controller>
        public IEnumerable<Birim> Get()
        {
            return _birimService.GetList();
        }

        // GET api/<controller>/5
        public Birim Get(int id)
        {
            return _birimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Birim birim)
        {
            return _birimService.Add(birim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Birim birim)
        {
            return _birimService.Update(birim);
        }

        public int Delete(int id)
        {
            return _birimService.DeleteSoft(id);
        }

        [Route("api/birim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _birimService.Delete(id);
        }
    }
}
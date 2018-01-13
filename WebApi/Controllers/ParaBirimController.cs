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
    public class ParaBirimController : ApiController
    {
        IParaBirimService _paraBirimService;

        public ParaBirimController(IParaBirimService paraBirimService)
        {
            _paraBirimService = paraBirimService;
        }

        // GET api/<controller>
        public IEnumerable<ParaBirim> Get()
        {
            return _paraBirimService.GetList();
        }

        // GET api/<controller>/5
        public ParaBirim Get(int id)
        {
            return _paraBirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ParaBirim paraBirim)
        {
            return _paraBirimService.Add(paraBirim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ParaBirim paraBirim)
        {
            return _paraBirimService.Update(paraBirim);
        }

        public int Delete(int id)
        {
            return _paraBirimService.DeleteSoft(id);
        }

        [Route("api/parabirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _paraBirimService.Delete(id);
        }
    }
}
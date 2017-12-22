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

    public class KisimController : ApiController
    {
        IKisimService _kisimService;

        public KisimController(IKisimService kisimService)
        {
            _kisimService = kisimService;
        }

        // GET api/<controller>
        public IEnumerable<Kisim> Get()
        {
            return _kisimService.GetList();
        }

        // GET api/<controller>/5
        public Kisim Get(int id)
        {
            return _kisimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kisim kisim)
        {
            return _kisimService.Add(kisim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kisim kisim)
        {
            return _kisimService.Update(kisim);
        }

        public int Delete(int id)
        {
            return _kisimService.DeleteSoft(id);
        }

        [Route("api/kisim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kisimService.Delete(id);
        }
    }
}
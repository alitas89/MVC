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
    public class HurdaController : ApiController
    {
        IHurdaService _hurdaService;

        public HurdaController(IHurdaService hurdaService)
        {
            _hurdaService = hurdaService;
        }

        // GET api/<controller>
        public IEnumerable<Hurda> Get()
        {
            return _hurdaService.GetList();
        }

        // GET api/<controller>/5
        public Hurda Get(int id)
        {
            return _hurdaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hurda hurda)
        {
            return _hurdaService.Add(hurda);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hurda hurda)
        {
            return _hurdaService.Update(hurda);
        }

        public int Delete(int id)
        {
            return _hurdaService.DeleteSoft(id);
        }

        [Route("api/hurda/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hurdaService.Delete(id);
        }
    }
}
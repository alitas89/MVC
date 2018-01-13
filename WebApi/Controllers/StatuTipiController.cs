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
    public class StatuTipiController : ApiController
    {
        IStatuTipiService _statuTipiService;

        public StatuTipiController(IStatuTipiService statuTipiService)
        {
            _statuTipiService = statuTipiService;
        }

        // GET api/<controller>
        public IEnumerable<StatuTipi> Get()
        {
            return _statuTipiService.GetList();
        }

        // GET api/<controller>/5
        public StatuTipi Get(int id)
        {
            return _statuTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]StatuTipi statuTipi)
        {
            return _statuTipiService.Add(statuTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]StatuTipi statuTipi)
        {
            return _statuTipiService.Update(statuTipi);
        }

        public int Delete(int id)
        {
            return _statuTipiService.DeleteSoft(id);
        }

        [Route("api/statutipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _statuTipiService.Delete(id);
        }
    }
}
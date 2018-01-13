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
    public class BakimRiskiController : ApiController
    {
        IBakimRiskiService _bakimRiskiService;

        public BakimRiskiController(IBakimRiskiService bakimRiskiService)
        {
            _bakimRiskiService = bakimRiskiService;
        }

        // GET api/<controller>
        public IEnumerable<BakimRiski> Get()
        {
            return _bakimRiskiService.GetList();
        }

        // GET api/<controller>/5
        public BakimRiski Get(int id)
        {
            return _bakimRiskiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimRiski bakimRiski)
        {
            return _bakimRiskiService.Add(bakimRiski);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimRiski bakimRiski)
        {
            return _bakimRiskiService.Update(bakimRiski);
        }

        public int Delete(int id)
        {
            return _bakimRiskiService.DeleteSoft(id);
        }

        [Route("api/bakimriski/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimRiskiService.Delete(id);
        }
    }
}
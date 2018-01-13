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
    public class RiskTipiController : ApiController
    {
        IRiskTipiService _riskTipiService;

        public RiskTipiController(IRiskTipiService riskTipiService)
        {
            _riskTipiService = riskTipiService;
        }

        // GET api/<controller>
        public IEnumerable<RiskTipi> Get()
        {
            return _riskTipiService.GetList();
        }

        // GET api/<controller>/5
        public RiskTipi Get(int id)
        {
            return _riskTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]RiskTipi riskTipi)
        {
            return _riskTipiService.Add(riskTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]RiskTipi riskTipi)
        {
            return _riskTipiService.Update(riskTipi);
        }

        public int Delete(int id)
        {
            return _riskTipiService.DeleteSoft(id);
        }

        [Route("api/risktipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _riskTipiService.Delete(id);
        }
    }
}
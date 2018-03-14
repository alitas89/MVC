using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;
using System.Linq.Dynamic;

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
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _riskTipiService.GetCount(filter) : _riskTipiService.GetCount();
            var d = _riskTipiService.GetListPagination(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });
            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
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
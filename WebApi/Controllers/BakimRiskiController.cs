﻿using System;
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
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _bakimRiskiService.GetCount(filterCol, filterVal) : _bakimRiskiService.GetCount();
            var d = _bakimRiskiService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
                limit = limit,
                offset = offset,
                order = order
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
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
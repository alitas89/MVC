﻿using BusinessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TeslimSekliController : ApiController
    {
        ITeslimSekliService _teslimSekliService;

        public TeslimSekliController(ITeslimSekliService teslimSekliService)
        {
            _teslimSekliService = teslimSekliService;
        }

        // GET api/<controller>
        public IEnumerable<TeslimSekli> Get()
        {
            return _teslimSekliService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _teslimSekliService.GetCount(filterCol, filterVal) : _teslimSekliService.GetCount();
            var d = _teslimSekliService.GetListPagination(new PagingParams()
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
        public TeslimSekli Get(int id)
        {
            return _teslimSekliService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]TeslimSekli teslimSekli)
        {
            return _teslimSekliService.Add(teslimSekli);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]TeslimSekli teslimSekli)
        {
            return _teslimSekliService.Update(teslimSekli);
        }

        public int Delete(int id)
        {
            return _teslimSekliService.DeleteSoft(id);
        }

        [Route("api/teslimsekli/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _teslimSekliService.Delete(id);
        }
    }
}
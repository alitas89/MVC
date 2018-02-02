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
using EntityLayer.Concrete.Varlik;

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
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _birimService.GetCount(filterCol, filterVal) : _birimService.GetCount();
            var d = _birimService.GetListPagination(new PagingParams()
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
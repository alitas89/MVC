﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class IsletmeController : ApiController
    {
        IIsletmeService _isletmeService;

        public IsletmeController(IIsletmeService isletmeService)
        {
            _isletmeService = isletmeService;
        }

        // GET api/<controller>
        public IEnumerable<Isletme> Get()
        {
            return _isletmeService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _isletmeService.GetCount(filterCol, filterVal) : _isletmeService.GetCount();
            var d = _isletmeService.GetListPagination(new PagingParams()
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
        public Isletme Get(int id)
        {
            return _isletmeService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Isletme isletme)
        {
            return _isletmeService.Add(isletme);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Isletme isletme)
        {
            return _isletmeService.Update(isletme);
        }

        public int Delete(int id)
        {
            return _isletmeService.DeleteSoft(id);
        }

        [Route("api/ısletme/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isletmeService.Delete(id);
        }
    }
}
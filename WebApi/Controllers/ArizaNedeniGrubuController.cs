﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class ArizaNedeniGrubuController : ApiController
    {
        IArizaNedeniGrubuService _arizaNedeniGrubuService;

        public ArizaNedeniGrubuController(IArizaNedeniGrubuService arizaNedeniGrubuService)
        {
            _arizaNedeniGrubuService = arizaNedeniGrubuService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaNedeniGrubu> Get()
        {
            return _arizaNedeniGrubuService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _arizaNedeniGrubuService.GetCount(filterCol, filterVal) : _arizaNedeniGrubuService.GetCount();
            var d = _arizaNedeniGrubuService.GetListPagination(new PagingParams()
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
        public ArizaNedeniGrubu Get(int id)
        {
            return _arizaNedeniGrubuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ArizaNedeniGrubu arizaNedeniGrubu)
        {
            return _arizaNedeniGrubuService.Add(arizaNedeniGrubu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ArizaNedeniGrubu arizaNedeniGrubu)
        {
            return _arizaNedeniGrubuService.Update(arizaNedeniGrubu);
        }

        public int Delete(int id)
        {
            return _arizaNedeniGrubuService.DeleteSoft(id);
        }

        [Route("api/arizanedenigrubu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _arizaNedeniGrubuService.Delete(id);
        }
    }
}
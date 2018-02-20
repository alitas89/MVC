﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace WebApi.Controllers
{
    public class MesaiController : ApiController
    {
        IMesaiService _mesaiService;

        public MesaiController(IMesaiService mesaiService)
        {
            _mesaiService = mesaiService;
        }

        // GET api/<controller>
        public IEnumerable<Mesai> Get()
        {
            return _mesaiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _mesaiService.GetCount(filterCol, filterVal) : _mesaiService.GetCount();
            var d = _mesaiService.GetListPagination(new PagingParams()
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
        public Mesai Get(int id)
        {
            return _mesaiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Mesai mesai)
        {
            return _mesaiService.Add(mesai);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Mesai mesai)
        {
            return _mesaiService.Update(mesai);
        }

        public int Delete(int id)
        {
            return _mesaiService.DeleteSoft(id);
        }

        [Route("api/mesai/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _mesaiService.Delete(id);
        }
    }
}
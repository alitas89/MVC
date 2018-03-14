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
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class KaynakPozisyonuController : ApiController
    {
        IKaynakPozisyonuService _kaynakPozisyonuService;

        public KaynakPozisyonuController(IKaynakPozisyonuService kaynakPozisyonuService)
        {
            _kaynakPozisyonuService = kaynakPozisyonuService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakPozisyonu> Get()
        {
            return _kaynakPozisyonuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakPozisyonuService.GetCount(filter) : _kaynakPozisyonuService.GetCount();
            var d = _kaynakPozisyonuService.GetListPagination(new PagingParams()
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
        public KaynakPozisyonu Get(int id)
        {
            return _kaynakPozisyonuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakPozisyonu kaynakPozisyonu)
        {
            return _kaynakPozisyonuService.Add(kaynakPozisyonu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakPozisyonu kaynakPozisyonu)
        {
            return _kaynakPozisyonuService.Update(kaynakPozisyonu);
        }

        public int Delete(int id)
        {
            return _kaynakPozisyonuService.DeleteSoft(id);
        }

        [Route("api/kaynakpozisyonu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakPozisyonuService.Delete(id);
        }
    }
}
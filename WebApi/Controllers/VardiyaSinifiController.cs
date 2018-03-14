using System;
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
    public class VardiyaSinifiController : ApiController
    {
        IVardiyaSinifiService _vardiyaSinifiService;

        public VardiyaSinifiController(IVardiyaSinifiService vardiyaSinifiService)
        {
            _vardiyaSinifiService = vardiyaSinifiService;
        }

        // GET api/<controller>
        public IEnumerable<VardiyaSinifi> Get()
        {
            return _vardiyaSinifiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _vardiyaSinifiService.GetCount(filter) : _vardiyaSinifiService.GetCount();
            var d = _vardiyaSinifiService.GetListPagination(new PagingParams()
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
        public VardiyaSinifi Get(int id)
        {
            return _vardiyaSinifiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VardiyaSinifi vardiyaSinifi)
        {
            return _vardiyaSinifiService.Add(vardiyaSinifi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VardiyaSinifi vardiyaSinifi)
        {
            return _vardiyaSinifiService.Update(vardiyaSinifi);
        }

        public int Delete(int id)
        {
            return _vardiyaSinifiService.DeleteSoft(id);
        }

        [Route("api/vardiyasinifi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _vardiyaSinifiService.Delete(id);
        }
    }
}
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
    public class KaynakSinifiController : ApiController
    {
        IKaynakSinifiService _kaynakSinifiService;

        public KaynakSinifiController(IKaynakSinifiService kaynakSinifiService)
        {
            _kaynakSinifiService = kaynakSinifiService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakSinifi> Get()
        {
            return _kaynakSinifiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakSinifiService.GetCount(filter) : _kaynakSinifiService.GetCount();
            var d = _kaynakSinifiService.GetListPagination(new PagingParams()
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
        public KaynakSinifi Get(int id)
        {
            return _kaynakSinifiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakSinifi kaynakSinifi)
        {
            return _kaynakSinifiService.Add(kaynakSinifi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakSinifi kaynakSinifi)
        {
            return _kaynakSinifiService.Update(kaynakSinifi);
        }

        public int Delete(int id)
        {
            return _kaynakSinifiService.DeleteSoft(id);
        }

        [Route("api/kaynaksinifi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakSinifiService.Delete(id);
        }
    }
}
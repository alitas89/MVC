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
    public class BilgilendirmeTuruController : ApiController
    {
        IBilgilendirmeTuruService _bilgilendirmeTuruService;

        public BilgilendirmeTuruController(IBilgilendirmeTuruService bilgilendirmeTuruService)
        {
            _bilgilendirmeTuruService = bilgilendirmeTuruService;
        }

        // GET api/<controller>
        public IEnumerable<BilgilendirmeTuru> Get()
        {
            return _bilgilendirmeTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bilgilendirmeTuruService.GetCount(filter) : _bilgilendirmeTuruService.GetCount();
            var d = _bilgilendirmeTuruService.GetListPagination(new PagingParams()
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
        public BilgilendirmeTuru Get(int id)
        {
            return _bilgilendirmeTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BilgilendirmeTuru bilgilendirmeTuru)
        {
            return _bilgilendirmeTuruService.Add(bilgilendirmeTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BilgilendirmeTuru bilgilendirmeTuru)
        {
            return _bilgilendirmeTuruService.Update(bilgilendirmeTuru);
        }

        public int Delete(int id)
        {
            return _bilgilendirmeTuruService.DeleteSoft(id);
        }

        [Route("api/bilgilendirmeturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bilgilendirmeTuruService.Delete(id);
        }
    }
}
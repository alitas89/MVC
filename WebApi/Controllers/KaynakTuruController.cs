using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class KaynakTuruController : ApiController
    {
        IKaynakTuruService _kaynakTuruService;

        public KaynakTuruController(IKaynakTuruService kaynakTuruService)
        {
            _kaynakTuruService = kaynakTuruService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakTuru> Get()
        {
            return _kaynakTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _kaynakTuruService.GetCount(filterCol, filterVal) : _kaynakTuruService.GetCount();
            var d = _kaynakTuruService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public KaynakTuru Get(int id)
        {
            return _kaynakTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakTuru kaynakTuru)
        {
            return _kaynakTuruService.Add(kaynakTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakTuru kaynakTuru)
        {
            return _kaynakTuruService.Update(kaynakTuru);
        }

        public int Delete(int id)
        {
            return _kaynakTuruService.DeleteSoft(id);
        }

        [Route("api/kaynakturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakTuruService.Delete(id);
        }
    }
}
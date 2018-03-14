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
    public class KaynakDurumuController : ApiController
    {
        IKaynakDurumuService _kaynakDurumuService;

        public KaynakDurumuController(IKaynakDurumuService kaynakDurumuService)
        {
            _kaynakDurumuService = kaynakDurumuService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakDurumu> Get()
        {
            return _kaynakDurumuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakDurumuService.GetCount(filter) : _kaynakDurumuService.GetCount();
            var d = _kaynakDurumuService.GetListPagination(new PagingParams()
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
        public KaynakDurumu Get(int id)
        {
            return _kaynakDurumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakDurumu kaynakDurumu)
        {
            return _kaynakDurumuService.Add(kaynakDurumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakDurumu kaynakDurumu)
        {
            return _kaynakDurumuService.Update(kaynakDurumu);
        }

        public int Delete(int id)
        {
            return _kaynakDurumuService.DeleteSoft(id);
        }

        [Route("api/kaynakdurumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakDurumuService.Delete(id);
        }
    }
}
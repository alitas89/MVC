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
    public class KaynakTipiController : ApiController
    {
        IKaynakTipiService _kaynakTipiService;

        public KaynakTipiController(IKaynakTipiService kaynakTipiService)
        {
            _kaynakTipiService = kaynakTipiService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakTipi> Get()
        {
            return _kaynakTipiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakTipiService.GetCount(filter) : _kaynakTipiService.GetCount();
            var d = _kaynakTipiService.GetListPagination(new PagingParams()
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
        public KaynakTipi Get(int id)
        {
            return _kaynakTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakTipi kaynakTipi)
        {
            return _kaynakTipiService.Add(kaynakTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakTipi kaynakTipi)
        {
            return _kaynakTipiService.Update(kaynakTipi);
        }

        public int Delete(int id)
        {
            return _kaynakTipiService.DeleteSoft(id);
        }

        [Route("api/kaynaktipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakTipiService.Delete(id);
        }
    }
}
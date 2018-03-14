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
    public class BakimEkibiController : ApiController
    {
        IBakimEkibiService _bakimEkibiService;

        public BakimEkibiController(IBakimEkibiService bakimEkibiService)
        {
            _bakimEkibiService = bakimEkibiService;
        }

        // GET api/<controller>
        public IEnumerable<BakimEkibi> Get()
        {
            return _bakimEkibiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimEkibiService.GetCount(filter) : _bakimEkibiService.GetCount();
            var d = _bakimEkibiService.GetListPagination(new PagingParams()
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
        public BakimEkibi Get(int id)
        {
            return _bakimEkibiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimEkibi bakimEkibi)
        {
            return _bakimEkibiService.Add(bakimEkibi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimEkibi bakimEkibi)
        {
            return _bakimEkibiService.Update(bakimEkibi);
        }

        public int Delete(int id)
        {
            return _bakimEkibiService.DeleteSoft(id);
        }

        [Route("api/bakimekibi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimEkibiService.Delete(id);
        }
    }
}
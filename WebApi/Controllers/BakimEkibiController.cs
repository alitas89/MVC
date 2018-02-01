using System;
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
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _bakimEkibiService.GetCount(filterCol, filterVal) : _bakimEkibiService.GetCount();
            var d = _bakimEkibiService.GetListPagination(new PagingParams()
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
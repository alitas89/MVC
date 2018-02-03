using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace WebApi.Controllers
{
    public class OlcuBirimController : ApiController
    {
        IOlcuBirimService _olcuBirimService;

        public OlcuBirimController(IOlcuBirimService olcuBirimService)
        {
            _olcuBirimService = olcuBirimService;
        }

        // GET api/<controller>
        public IEnumerable<OlcuBirim> Get()
        {
            return _olcuBirimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _olcuBirimService.GetCount(filterCol, filterVal) : _olcuBirimService.GetCount();
            var d = _olcuBirimService.GetListPagination(new PagingParams()
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
        public OlcuBirim Get(int id)
        {
            return _olcuBirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]OlcuBirim olcuBirim)
        {
            return _olcuBirimService.Add(olcuBirim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]OlcuBirim olcuBirim)
        {
            return _olcuBirimService.Update(olcuBirim);
        }

        public int Delete(int id)
        {
            return _olcuBirimService.DeleteSoft(id);
        }

        [System.Web.Mvc.Route("api/olcubirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _olcuBirimService.Delete(id);
        }
    }
}
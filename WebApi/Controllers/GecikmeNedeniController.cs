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
    public class GecikmeNedeniController : ApiController
    {
        IGecikmeNedeniService _gecikmeNedeniService;

        public GecikmeNedeniController(IGecikmeNedeniService gecikmeNedeniService)
        {
            _gecikmeNedeniService = gecikmeNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<GecikmeNedeni> Get()
        {
            return _gecikmeNedeniService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _gecikmeNedeniService.GetCount(filterCol, filterVal) : _gecikmeNedeniService.GetCount();
            var d = _gecikmeNedeniService.GetListPagination(new PagingParams()
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
        public GecikmeNedeni Get(int id)
        {
            return _gecikmeNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]GecikmeNedeni gecikmeNedeni)
        {
            return _gecikmeNedeniService.Add(gecikmeNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]GecikmeNedeni gecikmeNedeni)
        {
            return _gecikmeNedeniService.Update(gecikmeNedeni);
        }

        public int Delete(int id)
        {
            return _gecikmeNedeniService.DeleteSoft(id);
        }

        [Route("api/gecikmenedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _gecikmeNedeniService.Delete(id);
        }
    }
}
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
    public class GonderimFormatiController : ApiController
    {
        IGonderimFormatiService _gonderimFormatiService;

        public GonderimFormatiController(IGonderimFormatiService gonderimFormatiService)
        {
            _gonderimFormatiService = gonderimFormatiService;
        }

        // GET api/<controller>
        public IEnumerable<GonderimFormati> Get()
        {
            return _gonderimFormatiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _gonderimFormatiService.GetCount(filter) : _gonderimFormatiService.GetCount();
            var d = _gonderimFormatiService.GetListPagination(new PagingParams()
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
        public GonderimFormati Get(int id)
        {
            return _gonderimFormatiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Add(gonderimFormati);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Update(gonderimFormati);
        }

        public int Delete(int id)
        {
            return _gonderimFormatiService.DeleteSoft(id);
        }

        [Route("api/gonderimformati/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _gonderimFormatiService.Delete(id);
        }
    }
}
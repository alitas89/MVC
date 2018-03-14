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
    public class BakimOncelikController : ApiController
    {
        IBakimOncelikService _bakimOncelikService;

        public BakimOncelikController(IBakimOncelikService bakimOncelikService)
        {
            _bakimOncelikService = bakimOncelikService;
        }

        // GET api/<controller>
        public IEnumerable<BakimOncelik> Get()
        {
            return _bakimOncelikService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimOncelikService.GetCount(filter) : _bakimOncelikService.GetCount();
            var d = _bakimOncelikService.GetListPagination(new PagingParams()
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
        public BakimOncelik Get(int id)
        {
            return _bakimOncelikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimOncelik bakimOncelik)
        {
            return _bakimOncelikService.Add(bakimOncelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimOncelik bakimOncelik)
        {
            return _bakimOncelikService.Update(bakimOncelik);
        }

        public int Delete(int id)
        {
            return _bakimOncelikService.DeleteSoft(id);
        }

        [Route("api/bakimoncelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimOncelikService.Delete(id);
        }
    }
}
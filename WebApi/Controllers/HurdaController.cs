using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class HurdaController : ApiController
    {
        IHurdaService _hurdaService;

        public HurdaController(IHurdaService hurdaService)
        {
            _hurdaService = hurdaService;
        }

        // GET api/<controller>
        public IEnumerable<Hurda> Get()
        {
            return _hurdaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _hurdaService.GetCount(filter) : _hurdaService.GetCount();
            var d = _hurdaService.GetListPagination(new PagingParams()
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
        public Hurda Get(int id)
        {
            return _hurdaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hurda hurda)
        {
            return _hurdaService.Add(hurda);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hurda hurda)
        {
            return _hurdaService.Update(hurda);
        }

        public int Delete(int id)
        {
            return _hurdaService.DeleteSoft(id);
        }

        [Route("api/hurda/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hurdaService.Delete(id);
        }
    }
}
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
    public class DurusNedeniController : ApiController
    {
        IDurusNedeniService _durusNedeniService;

        public DurusNedeniController(IDurusNedeniService durusNedeniService)
        {
            _durusNedeniService = durusNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<DurusNedeni> Get()
        {
            return _durusNedeniService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _durusNedeniService.GetCount(filter) : _durusNedeniService.GetCount();
            var d = _durusNedeniService.GetListPagination(new PagingParams()
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
        public DurusNedeni Get(int id)
        {
            return _durusNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]DurusNedeni durusNedeni)
        {
            return _durusNedeniService.Add(durusNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]DurusNedeni durusNedeni)
        {
            return _durusNedeniService.Update(durusNedeni);
        }

        public int Delete(int id)
        {
            return _durusNedeniService.DeleteSoft(id);
        }

        [Route("api/durusnedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _durusNedeniService.Delete(id);
        }
    }
}
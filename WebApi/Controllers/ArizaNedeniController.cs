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
    public class ArizaNedeniController : ApiController
    {
        IArizaNedeniService _arizaNedeniService;

        public ArizaNedeniController(IArizaNedeniService arizaNedeniService)
        {
            _arizaNedeniService = arizaNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaNedeni> Get()
        {
            return _arizaNedeniService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _arizaNedeniService.GetCount(filterCol, filterVal) : _arizaNedeniService.GetCount();
            var d = _arizaNedeniService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public ArizaNedeni Get(int id)
        {
            return _arizaNedeniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ArizaNedeni arizaNedeni)
        {
            return _arizaNedeniService.Add(arizaNedeni);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ArizaNedeni arizaNedeni)
        {
            return _arizaNedeniService.Update(arizaNedeni);
        }

        public int Delete(int id)
        {
            return _arizaNedeniService.DeleteSoft(id);
        }

        [Route("api/arizanedeni/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _arizaNedeniService.Delete(id);
        }
    }
}
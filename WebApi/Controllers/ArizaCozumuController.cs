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
    public class ArizaCozumuController : ApiController
    {
        IArizaCozumuService _arizaCozumuService;

        public ArizaCozumuController(IArizaCozumuService arizaCozumuService)
        {
            _arizaCozumuService = arizaCozumuService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaCozumu> Get()
        {
            return _arizaCozumuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _arizaCozumuService.GetCount(filterCol, filterVal) : _arizaCozumuService.GetCount();
            var d = _arizaCozumuService.GetListPagination(new PagingParams()
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
        public ArizaCozumu Get(int id)
        {
            return _arizaCozumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ArizaCozumu arizaCozumu)
        {
            return _arizaCozumuService.Add(arizaCozumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ArizaCozumu arizaCozumu)
        {
            return _arizaCozumuService.Update(arizaCozumu);
        }

        public int Delete(int id)
        {
            return _arizaCozumuService.DeleteSoft(id);
        }

        [Route("api/arizacozumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _arizaCozumuService.Delete(id);
        }
    }
}
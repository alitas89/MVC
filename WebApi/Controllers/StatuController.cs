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
    public class StatuController : ApiController
    {
        IStatuService _statuService;

        public StatuController(IStatuService statuService)
        {
            _statuService = statuService;
        }

        // GET api/<controller>
        public IEnumerable<Statu> Get()
        {
            return _statuService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _statuService.GetCount(filterCol, filterVal) : _statuService.GetCount();
            var d = _statuService.GetListPagination(new PagingParams()
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
        public Statu Get(int id)
        {
            return _statuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Statu statu)
        {
            return _statuService.Add(statu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Statu statu)
        {
            return _statuService.Update(statu);
        }

        public int Delete(int id)
        {
            return _statuService.DeleteSoft(id);
        }

        [Route("api/statu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _statuService.Delete(id);
        }
    }
}
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
    public class DurusKismiController : ApiController
    {
        IDurusKismiService _durusKismiService;

        public DurusKismiController(IDurusKismiService durusKismiService)
        {
            _durusKismiService = durusKismiService;
        }

        // GET api/<controller>
        public IEnumerable<DurusKismi> Get()
        {
            return _durusKismiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _durusKismiService.GetCount(filterCol, filterVal) : _durusKismiService.GetCount();
            var d = _durusKismiService.GetListPagination(new PagingParams()
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
        public DurusKismi Get(int id)
        {
            return _durusKismiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]DurusKismi durusKismi)
        {
            return _durusKismiService.Add(durusKismi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]DurusKismi durusKismi)
        {
            return _durusKismiService.Update(durusKismi);
        }

        public int Delete(int id)
        {
            return _durusKismiService.DeleteSoft(id);
        }

        [Route("api/duruskismi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _durusKismiService.Delete(id);
        }
    }
}
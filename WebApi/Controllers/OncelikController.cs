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

namespace WebApi.Controllers
{
    public class OncelikController : ApiController
    {
        IOncelikService _oncelikService;

        public OncelikController(IOncelikService oncelikService)
        {
            _oncelikService = oncelikService;
        }

        // GET api/<controller>
        public IEnumerable<Oncelik> Get()
        {
            return _oncelikService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _oncelikService.GetCount(filterCol, filterVal) : _oncelikService.GetCount();
            var d = _oncelikService.GetListPagination(new PagingParams()
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
        public Oncelik Get(int id)
        {
            return _oncelikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Oncelik oncelik)
        {
            return _oncelikService.Add(oncelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Oncelik oncelik)
        {
            return _oncelikService.Update(oncelik);
        }

        public int Delete(int id)
        {
            return _oncelikService.DeleteSoft(id);
        }

        [Route("api/oncelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _oncelikService.Delete(id);
        }
    }
}
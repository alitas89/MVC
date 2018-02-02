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
    public class ParaBirimController : ApiController
    {
        IParaBirimService _paraBirimService;

        public ParaBirimController(IParaBirimService paraBirimService)
        {
            _paraBirimService = paraBirimService;
        }

        // GET api/<controller>
        public IEnumerable<ParaBirim> Get()
        {
            return _paraBirimService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _paraBirimService.GetCount(filterCol, filterVal) : _paraBirimService.GetCount();
            var d = _paraBirimService.GetListPagination(new PagingParams()
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
        public ParaBirim Get(int id)
        {
            return _paraBirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ParaBirim paraBirim)
        {
            return _paraBirimService.Add(paraBirim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ParaBirim paraBirim)
        {
            return _paraBirimService.Update(paraBirim);
        }

        public int Delete(int id)
        {
            return _paraBirimService.DeleteSoft(id);
        }

        [Route("api/parabirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _paraBirimService.Delete(id);
        }
    }
}
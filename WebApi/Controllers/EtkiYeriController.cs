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
    public class EtkiYeriController : ApiController
    {
        IEtkiYeriService _etkiYeriService;

        public EtkiYeriController(IEtkiYeriService etkiYeriService)
        {
            _etkiYeriService = etkiYeriService;
        }

        // GET api/<controller>
        public IEnumerable<EtkiYeri> Get()
        {
            return _etkiYeriService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _etkiYeriService.GetCount(filter) : _etkiYeriService.GetCount();
            var d = _etkiYeriService.GetListPagination(new PagingParams()
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
        public EtkiYeri Get(int id)
        {
            return _etkiYeriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]EtkiYeri etkiYeri)
        {
            return _etkiYeriService.Add(etkiYeri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]EtkiYeri etkiYeri)
        {
            return _etkiYeriService.Update(etkiYeri);
        }

        public int Delete(int id)
        {
            return _etkiYeriService.DeleteSoft(id);
        }

        [Route("api/etkiyeri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _etkiYeriService.Delete(id);
        }
    }
}
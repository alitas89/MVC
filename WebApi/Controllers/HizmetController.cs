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
    public class HizmetController : ApiController
    {
        IHizmetService _hizmetService;

        public HizmetController(IHizmetService hizmetService)
        {
            _hizmetService = hizmetService;
        }

        // GET api/<controller>
        public IEnumerable<Hizmet> Get()
        {
            return _hizmetService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _hizmetService.GetCount(filter) : _hizmetService.GetCount();
            var d = _hizmetService.GetListPagination(new PagingParams()
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
        public Hizmet Get(int id)
        {
            return _hizmetService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Add(hizmet);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Update(hizmet);
        }

        public int Delete(int id)
        {
            return _hizmetService.DeleteSoft(id);
        }

        [Route("api/hizmet/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hizmetService.Delete(id);
        }
    }
}
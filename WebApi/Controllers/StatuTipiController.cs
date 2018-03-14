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
    public class StatuTipiController : ApiController
    {
        IStatuTipiService _statuTipiService;

        public StatuTipiController(IStatuTipiService statuTipiService)
        {
            _statuTipiService = statuTipiService;
        }

        // GET api/<controller>
        public IEnumerable<StatuTipi> Get()
        {
            return _statuTipiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _statuTipiService.GetCount(filter) : _statuTipiService.GetCount();
            var d = _statuTipiService.GetListPagination(new PagingParams()
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
        public StatuTipi Get(int id)
        {
            return _statuTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]StatuTipi statuTipi)
        {
            return _statuTipiService.Add(statuTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]StatuTipi statuTipi)
        {
            return _statuTipiService.Update(statuTipi);
        }

        public int Delete(int id)
        {
            return _statuTipiService.DeleteSoft(id);
        }

        [Route("api/statutipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _statuTipiService.Delete(id);
        }
    }
}
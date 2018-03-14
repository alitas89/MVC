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
    public class IsTipiController : ApiController
    {
        IIsTipiService _isTipiService;

        public IsTipiController(IIsTipiService isTipiService)
        {
            _isTipiService = isTipiService;
        }

        // GET api/<controller>
        public IEnumerable<IsTipi> Get()
        {
            return _isTipiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isTipiService.GetCountDto(filter) : _isTipiService.GetCountDto();
            var d = _isTipiService.GetListPaginationDto(new PagingParams()
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
        public IsTipi Get(int id)
        {
            return _isTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTipi isTipi)
        {
            return _isTipiService.Add(isTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTipi isTipi)
        {
            return _isTipiService.Update(isTipi);
        }

        public int Delete(int id)
        {
            return _isTipiService.DeleteSoft(id);
        }

        [Route("api/istipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTipiService.Delete(id);
        }
    }
}
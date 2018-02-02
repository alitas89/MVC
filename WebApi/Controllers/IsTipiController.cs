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
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _isTipiService.GetCount(filterCol, filterVal) : _isTipiService.GetCount();
            var d = _isTipiService.GetListPagination(new PagingParams()
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

        [Route("api/ıstipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTipiService.Delete(id);
        }
    }
}
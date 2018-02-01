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
    public class UretimTipiController : ApiController
    {
        IUretimTipiService _uretimTipiService;

        public UretimTipiController(IUretimTipiService uretimTipiService)
        {
            _uretimTipiService = uretimTipiService;
        }

        // GET api/<controller>
        public IEnumerable<UretimTipi> Get()
        {
            return _uretimTipiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _uretimTipiService.GetCount(filterCol, filterVal) : _uretimTipiService.GetCount();
            var d = _uretimTipiService.GetListPagination(new PagingParams()
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
        public UretimTipi Get(int id)
        {
            return _uretimTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]UretimTipi uretimTipi)
        {
            return _uretimTipiService.Add(uretimTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]UretimTipi uretimTipi)
        {
            return _uretimTipiService.Update(uretimTipi);
        }

        public int Delete(int id)
        {
            return _uretimTipiService.DeleteSoft(id);
        }

        [Route("api/uretimtipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _uretimTipiService.Delete(id);
        }
    }
}
using BusinessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TeminSuresiController : ApiController
    {
        ITeminSuresiService _teminSuresiService;

        public TeminSuresiController(ITeminSuresiService teminSuresiService)
        {
            _teminSuresiService = teminSuresiService;
        }

        // GET api/<controller>
        public IEnumerable<TeminSuresi> Get()
        {
            return _teminSuresiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _teminSuresiService.GetCount(filterCol, filterVal) : _teminSuresiService.GetCount();
            var d = _teminSuresiService.GetListPagination(new PagingParams()
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
        public TeminSuresi Get(int id)
        {
            return _teminSuresiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]TeminSuresi teminSuresi)
        {
            return _teminSuresiService.Add(teminSuresi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]TeminSuresi teminSuresi)
        {
            return _teminSuresiService.Update(teminSuresi);
        }

        public int Delete(int id)
        {
            return _teminSuresiService.DeleteSoft(id);
        }

        [Route("api/teminsuresi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _teminSuresiService.Delete(id);
        }
    }
}
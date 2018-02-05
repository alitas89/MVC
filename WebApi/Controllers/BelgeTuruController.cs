using System.Collections.Generic;
using System.Web.Http;
using BusinessLayer.Abstract.Satinalma;
using EntityLayer.Concrete.Satinalma;
using System.Net.Http;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Net;

namespace WebApi.Controllers
{
    public class BelgeTuruController : ApiController
    {
        IBelgeTuruService _belgeTuruService;

        public BelgeTuruController(IBelgeTuruService belgeTuruService)
        {
            _belgeTuruService = belgeTuruService;
        }

        // GET api/<controller>
        public IEnumerable<BelgeTuru> Get()
        {
            return _belgeTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _belgeTuruService.GetCount(filterCol, filterVal) : _belgeTuruService.GetCount();
            var d = _belgeTuruService.GetListPagination(new PagingParams()
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
        public BelgeTuru Get(int id)
        {
            return _belgeTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BelgeTuru belgeTuru)
        {
            return _belgeTuruService.Add(belgeTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BelgeTuru belgeTuru)
        {
            return _belgeTuruService.Update(belgeTuru);
        }

        public int Delete(int id)
        {
            return _belgeTuruService.DeleteSoft(id);
        }

        [Route("api/belgeturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _belgeTuruService.Delete(id);
        }
    }
}
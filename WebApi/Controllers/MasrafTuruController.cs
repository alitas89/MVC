using BusinessLayer.Abstract.Satinalma;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MasrafTuruController : ApiController
    {
        IMasrafTuruService _masrafTuruService;

        public MasrafTuruController(IMasrafTuruService masrafTuruService)
        {
            _masrafTuruService = masrafTuruService;
        }

        // GET api/<controller>
        public IEnumerable<MasrafTuru> Get()
        {
            return _masrafTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _masrafTuruService.GetCountDto(filterCol, filterVal) : _masrafTuruService.GetCountDto();
            var d = _masrafTuruService.GetListPaginationDto(new PagingParams()
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
        public MasrafTuru Get(int id)
        {
            return _masrafTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MasrafTuru masrafTuru)
        {
            return _masrafTuruService.Add(masrafTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MasrafTuru masrafTuru)
        {
            return _masrafTuruService.Update(masrafTuru);
        }

        public int Delete(int id)
        {
            return _masrafTuruService.DeleteSoft(id);
        }

        [Route("api/masrafturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _masrafTuruService.Delete(id);
        }
    }
}
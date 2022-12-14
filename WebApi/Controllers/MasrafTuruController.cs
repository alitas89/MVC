using BusinessLayer.Abstract.Satinalma;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

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
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _masrafTuruService.GetCountDto(filter) : _masrafTuruService.GetCountDto();
            var d = _masrafTuruService.GetListPaginationDto(new PagingParams()
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
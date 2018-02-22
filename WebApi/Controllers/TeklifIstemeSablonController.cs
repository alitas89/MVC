using BusinessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TeklifIstemeSablonController : ApiController
    {
        ITeklifIstemeSablonService _teklifIstemeSablonService;

        public TeklifIstemeSablonController(ITeklifIstemeSablonService teklifIstemeSablonService)
        {
            _teklifIstemeSablonService = teklifIstemeSablonService;
        }

        // GET api/<controller>
        public IEnumerable<TeklifIstemeSablon> Get()
        {
            return _teklifIstemeSablonService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _teklifIstemeSablonService.GetCountDto(filterCol, filterVal) : _teklifIstemeSablonService.GetCountDto();
            var d = _teklifIstemeSablonService.GetListPaginationDto(new PagingParams()
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
        public TeklifIstemeSablon Get(int id)
        {
            return _teklifIstemeSablonService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]TeklifIstemeSablon teklifIstemeSablon)
        {
            return _teklifIstemeSablonService.Add(teklifIstemeSablon);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]TeklifIstemeSablon teklifIstemeSablon)
        {
            return _teklifIstemeSablonService.Update(teklifIstemeSablon);
        }

        public int Delete(int id)
        {
            return _teklifIstemeSablonService.DeleteSoft(id);
        }

        [Route("api/teklifıstemesablon/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _teklifIstemeSablonService.Delete(id);
        }
    }
}
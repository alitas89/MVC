//**WEB API CONTROLLER - TeslimYeri

using BusinessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class TeslimYeriController : ApiController
    {
        ITeslimYeriService _teslimYeriService;

        public TeslimYeriController(ITeslimYeriService teslimYeriService)
        {
            _teslimYeriService = teslimYeriService;
        }

        // GET api/<controller>
        public IEnumerable<TeslimYeri> Get()
        {
            return _teslimYeriService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _teslimYeriService.GetCountDto(filter) : _teslimYeriService.GetCountDto();
            var d = _teslimYeriService.GetListPaginationDto(new PagingParams()
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
        public TeslimYeri Get(int id)
        {
            return _teslimYeriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]TeslimYeri teslimYeri)
        {
            return _teslimYeriService.Add(teslimYeri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]TeslimYeri teslimYeri)
        {
            return _teslimYeriService.Update(teslimYeri);
        }

        public int Delete(int id)
        {
            return _teslimYeriService.DeleteSoft(id);
        }

        [Route("api/teslimyeri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _teslimYeriService.Delete(id);
        }
    }
}
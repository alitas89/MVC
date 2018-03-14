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
    public class TeslimSekliController : ApiController
    {
        ITeslimSekliService _teslimSekliService;

        public TeslimSekliController(ITeslimSekliService teslimSekliService)
        {
            _teslimSekliService = teslimSekliService;
        }

        // GET api/<controller>
        public IEnumerable<TeslimSekli> Get()
        {
            return _teslimSekliService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _teslimSekliService.GetCount(filter) : _teslimSekliService.GetCount();
            var d = _teslimSekliService.GetListPagination(new PagingParams()
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
        public TeslimSekli Get(int id)
        {
            return _teslimSekliService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]TeslimSekli teslimSekli)
        {
            return _teslimSekliService.Add(teslimSekli);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]TeslimSekli teslimSekli)
        {
            return _teslimSekliService.Update(teslimSekli);
        }

        public int Delete(int id)
        {
            return _teslimSekliService.DeleteSoft(id);
        }

        [Route("api/teslimsekli/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _teslimSekliService.Delete(id);
        }
    }
}
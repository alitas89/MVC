using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class AracServisController : ApiController
    {
        IAracServisService _aracServisService;

        public AracServisController(IAracServisService aracServisService)
        {
            _aracServisService = aracServisService;
        }

        // GET api/<controller>
        public IEnumerable<AracServis> Get()
        {
            return _aracServisService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _aracServisService.GetCountDto(filterCol, filterVal) : _aracServisService.GetCountDto();
            var d = _aracServisService.GetListPaginationDto(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public AracServis Get(int id)
        {
            return _aracServisService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AracServis aracServis)
        {
            return _aracServisService.Add(aracServis);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AracServis aracServis)
        {
            return _aracServisService.Update(aracServis);
        }

        public int Delete(int id)
        {
            return _aracServisService.DeleteSoft(id);
        }

        [Route("api/aracservis/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _aracServisService.Delete(id);
        }
    }
}
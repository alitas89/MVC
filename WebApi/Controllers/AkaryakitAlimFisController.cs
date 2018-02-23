using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class AkaryakitAlimFisController : ApiController
    {
        IAkaryakitAlimFisService _akaryakitAlimFisService;

        public AkaryakitAlimFisController(IAkaryakitAlimFisService akaryakitAlimFisService)
        {
            _akaryakitAlimFisService = akaryakitAlimFisService;
        }

        // GET api/<controller>
        public IEnumerable<AkaryakitAlimFis> Get()
        {
            return _akaryakitAlimFisService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _akaryakitAlimFisService.GetCount(filterCol, filterVal) : _akaryakitAlimFisService.GetCount();
            var d = _akaryakitAlimFisService.GetListPagination(new PagingParams()
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
        public AkaryakitAlimFis Get(int id)
        {
            return _akaryakitAlimFisService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AkaryakitAlimFis akaryakitAlimFis)
        {
            return _akaryakitAlimFisService.Add(akaryakitAlimFis);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AkaryakitAlimFis akaryakitAlimFis)
        {
            return _akaryakitAlimFisService.Update(akaryakitAlimFis);
        }

        public int Delete(int id)
        {
            return _akaryakitAlimFisService.DeleteSoft(id);
        }

        [Route("api/akaryakitalimfis/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _akaryakitAlimFisService.Delete(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class BakimEkibiUyeController : ApiController
    {
        IBakimEkibiUyeService _bakimEkibiUyeService;

        public BakimEkibiUyeController(IBakimEkibiUyeService bakimEkibiUyeService)
        {
            _bakimEkibiUyeService = bakimEkibiUyeService;
        }

        // GET api/<controller>
        public IEnumerable<BakimEkibiUye> Get()
        {
            return _bakimEkibiUyeService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimEkibiUyeService.GetCount(filter) : _bakimEkibiUyeService.GetCount();
            List<BakimEkibiUye> d = _bakimEkibiUyeService.GetListPagination(new PagingParams()
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
        public BakimEkibiUye Get(int id)
        {
            return _bakimEkibiUyeService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimEkibiTemp bakimEkibiTemp)
        {
            return _bakimEkibiUyeService.AddBakimEkibiUye(bakimEkibiTemp.BakimEkibiID, bakimEkibiTemp.arrKaynakID);
        }

        public int Delete(int id)
        {
            return _bakimEkibiUyeService.DeleteSoft(id);
        }

        [Route("api/bakimekibiuye/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimEkibiUyeService.Delete(id);
        }

        [Route("api/bakimekibiuye/getuyebybakimekibiid/{bakimEkibiID}")]
        [HttpGet]
        public string GetUyeByBakimEkibiID(int bakimEkibiID)
        {
            return _bakimEkibiUyeService.GetUyeByBakimEkibiID(bakimEkibiID);
        }
    }
}

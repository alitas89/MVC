using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;

namespace WebApi.Controllers
{
    public class MalzemeHareketController : ApiController
    {
        IMalzemeHareketService _malzemeHareketService;

        public MalzemeHareketController(IMalzemeHareketService malzemeHareketService)
        {
            _malzemeHareketService = malzemeHareketService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeHareket> Get()
        {
            return _malzemeHareketService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeHareketService.GetCountDto(filter) : _malzemeHareketService.GetCountDto();
            var d = _malzemeHareketService.GetListPaginationDto(new PagingParams()
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
        public MalzemeHareket Get(int id)
        {
            return _malzemeHareketService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeHareketTemp malzemeHareketTemp)
        {
            return _malzemeHareketService.AddMalzemeHareket(malzemeHareketTemp);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeHareketTemp malzemeHareketTemp)
        {
            return _malzemeHareketService.UpdateMalzemeHareket(malzemeHareketTemp);
        }

        public int Delete(int id)
        {
            return _malzemeHareketService.DeleteSoft(id);
        }

        [Route("api/malzemehareket/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeHareketService.Delete(id);
        }
    }
}

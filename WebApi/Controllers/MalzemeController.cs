using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using EntityLayer.Concrete.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;

namespace WebApi.Controllers
{
    public class MalzemeController : ApiController
    {
        IMalzemeService _malzemeService;

        public MalzemeController(IMalzemeService malzemeService)
        {
            _malzemeService = malzemeService;
        }

        // GET api/<controller>
        public IEnumerable<EntityLayer.Concrete.Malzeme.Malzeme> Get()
        {
            return _malzemeService.GetList();
        }

        [Route("api/malzeme/getmalzemeambardetay/{MalzemeID}")]
        [HttpGet]
        public IEnumerable<MalzemeAmbarDetay> GetMalzemeAmbarDetay(int MalzemeID)
        {
            return _malzemeService.GetMalzemeAmbarDetay(MalzemeID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeService.GetCountDto(filter) : _malzemeService.GetCountDto();
            var d = _malzemeService.GetListPaginationDto(new PagingParams()
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
        public Malzeme Get(int id)
        {
            return _malzemeService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Malzeme malzeme)
        {
            return _malzemeService.Add(malzeme);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Malzeme malzeme)
        {
            return _malzemeService.Update(malzeme);
        }

        public int Delete(int id)
        {
            return _malzemeService.DeleteSoft(id);
        }

        [Route("api/malzeme/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeService.Delete(id);
        }
    }
}
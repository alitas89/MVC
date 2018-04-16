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
    public class MalzemeHareketFisController : ApiController
    {
        IMalzemeHareketFisService _malzemeHareketFisService;

        public MalzemeHareketFisController(IMalzemeHareketFisService malzemeHareketFisService)
        {
            _malzemeHareketFisService = malzemeHareketFisService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeHareketFis> Get()
        {
            return _malzemeHareketFisService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeHareketFisService.GetCount(filter) : _malzemeHareketFisService.GetCount();
            var d = _malzemeHareketFisService.GetListPagination(new PagingParams()
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
        public MalzemeHareketFis Get(int id)
        {
            return _malzemeHareketFisService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeHareketFis malzemeHareketFis)
        {
            return _malzemeHareketFisService.Add(malzemeHareketFis);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeHareketFis malzemeHareketFis)
        {
            return _malzemeHareketFisService.Update(malzemeHareketFis);
        }

        public int Delete(int id)
        {
            return _malzemeHareketFisService.DeleteSoft(id);
        }

        [Route("api/malzemehareketfis/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeHareketFisService.Delete(id);
        }
    }
}

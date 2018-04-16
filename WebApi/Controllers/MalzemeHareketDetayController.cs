using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class MalzemeHareketDetayController : ApiController
    {
        IMalzemeHareketDetayService _malzemeHareketDetayService;

        public MalzemeHareketDetayController(IMalzemeHareketDetayService malzemeHareketDetayService)
        {
            _malzemeHareketDetayService = malzemeHareketDetayService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeHareketDetay> Get()
        {
            return _malzemeHareketDetayService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeHareketDetayService.GetCount(filter) : _malzemeHareketDetayService.GetCount();
            var d = _malzemeHareketDetayService.GetListPagination(new PagingParams()
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
        public MalzemeHareketDetay Get(int id)
        {
            return _malzemeHareketDetayService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeHareketDetay malzemeHareketDetay)
        {
            return _malzemeHareketDetayService.Add(malzemeHareketDetay);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeHareketDetay malzemeHareketDetay)
        {
            return _malzemeHareketDetayService.Update(malzemeHareketDetay);
        }

        public int Delete(int id)
        {
            return _malzemeHareketDetayService.DeleteSoft(id);
        }

        [Route("api/malzemehareketdetay/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeHareketDetayService.Delete(id);
        }
    }
}

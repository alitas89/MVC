using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class KaynakController : ApiController
    {
        IKaynakService _kaynakService;

        public KaynakController(IKaynakService kaynakService)
        {
            _kaynakService = kaynakService;
        }

        // GET api/<controller>
        public IEnumerable<Kaynak> Get()
        {
            return _kaynakService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _kaynakService.GetCount(filterCol, filterVal) : _kaynakService.GetCount();
            var d = _kaynakService.GetListPagination(new PagingParams()
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
        public Kaynak Get(int id)
        {
            return _kaynakService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kaynak kaynak)
        {
            return _kaynakService.Add(kaynak);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kaynak kaynak)
        {
            return _kaynakService.Update(kaynak);
        }

        public int Delete(int id)
        {
            return _kaynakService.DeleteSoft(id);
        }

        [Route("api/kaynak/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakService.Delete(id);
        }
    }
}
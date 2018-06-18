using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

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
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakService.GetCountDto(filter) : _kaynakService.GetCountDto();
            var d = _kaynakService.GetListPaginationDto(new PagingParams()
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


        [Route("api/kaynak/getlistkaynakhavekullaniciid")]
        public List<Kaynak> GetListKaynakHaveKullaniciID()
        {
            return _kaynakService.GetListKaynakHaveKullaniciID();
        }
    }
}
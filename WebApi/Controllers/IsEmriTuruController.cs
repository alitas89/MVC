using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class IsEmriTuruController : ApiController
    {
        IIsEmriTuruService _isEmriTuruService;

        public IsEmriTuruController(IIsEmriTuruService isEmriTuruService)
        {
            _isEmriTuruService = isEmriTuruService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmriTuru> Get()
        {
            return _isEmriTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _isEmriTuruService.GetCount(filterCol, filterVal) : _isEmriTuruService.GetCount();
            var d = _isEmriTuruService.GetListPagination(new PagingParams()
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
        public IsEmriTuru Get(int id)
        {
            return _isEmriTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruService.Add(isEmriTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruService.Update(isEmriTuru);
        }

        public int Delete(int id)
        {
            return _isEmriTuruService.DeleteSoft(id);
        }

        [Route("api/ısemrituru/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isEmriTuruService.Delete(id);
        }
    }
}
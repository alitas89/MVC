using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class VarlikTuruController : ApiController
    {
        IVarlikTuruService _varlikTuruService;

        public VarlikTuruController(IVarlikTuruService varlikTuruService)
        {
            _varlikTuruService = varlikTuruService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikTuru> Get()
        {
            return _varlikTuruService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _varlikTuruService.GetCount(filterCol, filterVal) : _varlikTuruService.GetCount();
            var d = _varlikTuruService.GetListPagination(new PagingParams()
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
        public VarlikTuru Get(int id)
        {
            return _varlikTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikTuru varlikTuru)
        {
            return _varlikTuruService.Add(varlikTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikTuru varlikTuru)
        {
            return _varlikTuruService.Update(varlikTuru);
        }

        public int Delete(int id)
        {
            return _varlikTuruService.DeleteSoft(id);
        }

        [Route("api/varlikturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikTuruService.Delete(id);
        }
    }
}
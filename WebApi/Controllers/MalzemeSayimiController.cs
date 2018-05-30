using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace WebApi.Controllers
{
    public class MalzemeSayimiController : ApiController
    {
        IMalzemeSayimiService _malzemeSayimiService;

        public MalzemeSayimiController(IMalzemeSayimiService malzemeSayimiService)
        {
            _malzemeSayimiService = malzemeSayimiService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeSayimi> Get()
        {
            return _malzemeSayimiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeSayimiService.GetCount(filter) : _malzemeSayimiService.GetCount();
            List<MalzemeSayimi> d = _malzemeSayimiService.GetListPagination(new PagingParams()
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
        public MalzemeSayimi Get(int id)
        {
            return _malzemeSayimiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeSayimi malzemeSayimi)
        {
            return _malzemeSayimiService.Add(malzemeSayimi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeSayimi malzemeSayimi)
        {
            return _malzemeSayimiService.Update(malzemeSayimi);
        }

        public int Delete(int id)
        {
            return _malzemeSayimiService.DeleteSoft(id);
        }

        [Route("api/malzemesayimi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeSayimiService.Delete(id);
        }
    }
}

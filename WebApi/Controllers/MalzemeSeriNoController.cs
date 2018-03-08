using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class MalzemeSeriNoController : ApiController
    {
        IMalzemeSeriNoService _malzemeSeriNoService;

        public MalzemeSeriNoController(IMalzemeSeriNoService malzemeSeriNoService)
        {
            _malzemeSeriNoService = malzemeSeriNoService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeSeriNo> Get()
        {
            return _malzemeSeriNoService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _malzemeSeriNoService.GetCount(filterCol, filterVal) : _malzemeSeriNoService.GetCount();
            var d = _malzemeSeriNoService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public MalzemeSeriNo Get(int id)
        {
            return _malzemeSeriNoService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeSeriNo malzemeSeriNo)
        {
            return _malzemeSeriNoService.Add(malzemeSeriNo);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeSeriNo malzemeSeriNo)
        {
            return _malzemeSeriNoService.Update(malzemeSeriNo);
        }

        public int Delete(int id)
        {
            return _malzemeSeriNoService.DeleteSoft(id);
        }

        [Route("api/malzemeserino/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeSeriNoService.Delete(id);
        }
    }
}
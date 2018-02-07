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

namespace WebApi.Controllers
{
    public class MalzemeStatuController : ApiController
    {
        IMalzemeStatuService _malzemeStatuService;

        public MalzemeStatuController(IMalzemeStatuService malzemeStatuService)
        {
            _malzemeStatuService = malzemeStatuService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeStatu> Get()
        {
            return _malzemeStatuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _malzemeStatuService.GetCount(filterCol, filterVal) : _malzemeStatuService.GetCount();
            var d = _malzemeStatuService.GetListPagination(new PagingParams()
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
        public MalzemeStatu Get(int id)
        {
            return _malzemeStatuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeStatu malzemeStatu)
        {
            return _malzemeStatuService.Add(malzemeStatu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeStatu malzemeStatu)
        {
            return _malzemeStatuService.Update(malzemeStatu);
        }

        public int Delete(int id)
        {
            return _malzemeStatuService.DeleteSoft(id);
        }

        [Route("api/malzemestatu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeStatuService.Delete(id);
        }
    }
}
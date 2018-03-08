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
    public class MalzemeGrupController : ApiController
    {
        IMalzemeGrupService _malzemeGrupService;

        public MalzemeGrupController(IMalzemeGrupService malzemeGrupService)
        {
            _malzemeGrupService = malzemeGrupService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeGrup> Get()
        {
            return _malzemeGrupService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _malzemeGrupService.GetCount(filterCol, filterVal) : _malzemeGrupService.GetCount();
            var d = _malzemeGrupService.GetListPagination(new PagingParams()
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
        public MalzemeGrup Get(int id)
        {
            return _malzemeGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeGrup malzemeGrup)
        {
            return _malzemeGrupService.Add(malzemeGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeGrup malzemeGrup)
        {
            return _malzemeGrupService.Update(malzemeGrup);
        }

        public int Delete(int id)
        {
            return _malzemeGrupService.DeleteSoft(id);
        }

        [Route("api/malzemegrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeGrupService.Delete(id);
        }
    }
}
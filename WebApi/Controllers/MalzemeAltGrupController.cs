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
    public class MalzemeAltGrupController : ApiController
    {
        IMalzemeAltGrupService _malzemeAltGrupService;

        public MalzemeAltGrupController(IMalzemeAltGrupService malzemeAltGrupService)
        {
            _malzemeAltGrupService = malzemeAltGrupService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeAltGrup> Get()
        {
            return _malzemeAltGrupService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _malzemeAltGrupService.GetCount(filterCol, filterVal) : _malzemeAltGrupService.GetCount();
            var d = _malzemeAltGrupService.GetListPagination(new PagingParams()
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
        public MalzemeAltGrup Get(int id)
        {
            return _malzemeAltGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeAltGrup malzemeAltGrup)
        {
            return _malzemeAltGrupService.Add(malzemeAltGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeAltGrup malzemeAltGrup)
        {
            return _malzemeAltGrupService.Update(malzemeAltGrup);
        }

        public int Delete(int id)
        {
            return _malzemeAltGrupService.DeleteSoft(id);
        }

        [Route("api/malzemealtgrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeAltGrupService.Delete(id);
        }
    }
}
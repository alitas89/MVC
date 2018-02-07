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
    public class MuhasebeHesapController : ApiController
    {
        IMuhasebeHesapService _muhasebeHesapService;

        public MuhasebeHesapController(IMuhasebeHesapService muhasebeHesapService)
        {
            _muhasebeHesapService = muhasebeHesapService;
        }

        // GET api/<controller>
        public IEnumerable<MuhasebeHesap> Get()
        {
            return _muhasebeHesapService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _muhasebeHesapService.GetCount(filterCol, filterVal) : _muhasebeHesapService.GetCount();
            var d = _muhasebeHesapService.GetListPagination(new PagingParams()
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
        public MuhasebeHesap Get(int id)
        {
            return _muhasebeHesapService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MuhasebeHesap muhasebeHesap)
        {
            return _muhasebeHesapService.Add(muhasebeHesap);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MuhasebeHesap muhasebeHesap)
        {
            return _muhasebeHesapService.Update(muhasebeHesap);
        }

        public int Delete(int id)
        {
            return _muhasebeHesapService.DeleteSoft(id);
        }

        [Route("api/muhasebehesap/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _muhasebeHesapService.Delete(id);
        }
    }
}
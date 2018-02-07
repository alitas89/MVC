using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace WebApi.Controllers
{
    public class EgitimController : ApiController
    {
        IEgitimService _egitimService;

        public EgitimController(IEgitimService egitimService)
        {
            _egitimService = egitimService;
        }

        // GET api/<controller>
        public IEnumerable<Egitim> Get()
        {
            return _egitimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _egitimService.GetCount(filterCol, filterVal) : _egitimService.GetCount();
            var d = _egitimService.GetListPagination(new PagingParams()
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
        public Egitim Get(int id)
        {
            return _egitimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Egitim egitim)
        {
            return _egitimService.Add(egitim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Egitim egitim)
        {
            return _egitimService.Update(egitim);
        }

        public int Delete(int id)
        {
            return _egitimService.DeleteSoft(id);
        }

        [Route("api/egitim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _egitimService.Delete(id);
        }
    }
}
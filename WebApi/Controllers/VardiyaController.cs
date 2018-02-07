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
    public class VardiyaController : ApiController
    {
        IVardiyaService _vardiyaService;

        public VardiyaController(IVardiyaService vardiyaService)
        {
            _vardiyaService = vardiyaService;
        }

        // GET api/<controller>
        public IEnumerable<Vardiya> Get()
        {
            return _vardiyaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _vardiyaService.GetCount(filterCol, filterVal) : _vardiyaService.GetCount();
            var d = _vardiyaService.GetListPagination(new PagingParams()
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
        public Vardiya Get(int id)
        {
            return _vardiyaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Vardiya vardiya)
        {
            return _vardiyaService.Add(vardiya);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Vardiya vardiya)
        {
            return _vardiyaService.Update(vardiya);
        }

        public int Delete(int id)
        {
            return _vardiyaService.DeleteSoft(id);
        }

        [Route("api/vardiya/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _vardiyaService.Delete(id);
        }
    }
}
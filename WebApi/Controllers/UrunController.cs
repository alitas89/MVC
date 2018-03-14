using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class UrunController : ApiController
    {
        IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        // GET api/<controller>
        public IEnumerable<Urun> Get()
        {
            return _urunService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _urunService.GetCount(filter) : _urunService.GetCount();
            var d = _urunService.GetListPagination(new PagingParams()
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
        public Urun Get(int id)
        {
            return _urunService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Urun urun)
        {
            return _urunService.Add(urun);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Urun urun)
        {
            return _urunService.Update(urun);
        }

        public int Delete(int id)
        {
            return _urunService.DeleteSoft(id);
        }

        [Route("api/urun/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _urunService.Delete(id);
        }
    }
}
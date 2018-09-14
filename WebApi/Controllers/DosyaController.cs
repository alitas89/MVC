using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class DosyaController : ApiController
    {
        IDosyaService _dosyaService;

        public DosyaController(IDosyaService dosyaService)
        {
            _dosyaService = dosyaService;
        }

        // GET api/<controller>
        public IEnumerable<Dosya> Get()
        {
            return _dosyaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _dosyaService.GetCount(filter) : _dosyaService.GetCount();
            List<Dosya> d = _dosyaService.GetListPagination(new PagingParams()
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
        public Dosya Get(int id)
        {
            return _dosyaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Dosya dosya)
        {
            return _dosyaService.Add(dosya);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Dosya dosya)
        {
            return _dosyaService.Update(dosya);
        }

        public int Delete(int id)
        {
            return _dosyaService.DeleteSoft(id);
        }

        [Route("api/dosya/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _dosyaService.Delete(id);
        }
    }
}

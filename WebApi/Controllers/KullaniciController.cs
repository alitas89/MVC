using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace WebApi.Controllers
{
    public class KullaniciController : ApiController
    {
        IKullaniciService _kullaniciService;

        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        // GET api/<controller>
        public IEnumerable<Kullanici> Get()
        {
            return _kullaniciService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kullaniciService.GetCount(filter) : _kullaniciService.GetCount();
            List<Kullanici> d = _kullaniciService.GetListPagination(new PagingParams()
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
        public Kullanici Get(int id)
        {
            return _kullaniciService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kullanici kullanici)
        {
            return _kullaniciService.Add(kullanici);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kullanici kullanici)
        {
            return _kullaniciService.Update(kullanici);
        }

        public int Delete(int id)
        {
            return _kullaniciService.DeleteSoft(id);
        }

        [Route("api/kullanici/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kullaniciService.Delete(id);
        }
    }
}
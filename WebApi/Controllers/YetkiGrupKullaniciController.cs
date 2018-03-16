using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace WebApi.Controllers
{
    public class YetkiGrupKullaniciController : ApiController
    {
        IYetkiGrupKullaniciService _yetkiGrupKullaniciService;

        public YetkiGrupKullaniciController(IYetkiGrupKullaniciService yetkiGrupKullaniciService)
        {
            _yetkiGrupKullaniciService = yetkiGrupKullaniciService;
        }

        // GET api/<controller>
        public IEnumerable<YetkiGrupKullanici> Get()
        {
            return _yetkiGrupKullaniciService.GetList();
        }

        // GET api/<controller>
        public IEnumerable<YetkiGrupKullanici> GetYetkiGrupByKullaniciId(int kullaniciId)
        {
            return _yetkiGrupKullaniciService.GetListByKullaniciId(kullaniciId);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns="")
        {
            int total = 0;
            total = filter.Length != 0 ? _yetkiGrupKullaniciService.GetCount(filter) : _yetkiGrupKullaniciService.GetCount();
            var d = _yetkiGrupKullaniciService.GetListPagination(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public YetkiGrupKullanici Get(int id)
        {
            return _yetkiGrupKullaniciService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]YetkiGrupKullanici yetkiGrupKullanici)
        {
            return _yetkiGrupKullaniciService.Add(yetkiGrupKullanici);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]YetkiGrupKullanici yetkiGrupKullanici)
        {
            return _yetkiGrupKullaniciService.Update(yetkiGrupKullanici);
        }

        public int Delete(int id)
        {
            return _yetkiGrupKullaniciService.DeleteSoft(id);
        }

        [Route("api/yetkigrupkullanici/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yetkiGrupKullaniciService.Delete(id);
        }
    }
}
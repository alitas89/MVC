using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - BildirimAksiyonSayfa

    public class BildirimAksiyonSayfaController : ApiController
    {
        IBildirimAksiyonSayfaService _bildirimAksiyonSayfaService;

        public BildirimAksiyonSayfaController(IBildirimAksiyonSayfaService bildirimAksiyonSayfaService)
        {
            _bildirimAksiyonSayfaService = bildirimAksiyonSayfaService;
        }

        // GET api/<controller>
        public IEnumerable<BildirimAksiyonSayfa> Get()
        {
            return _bildirimAksiyonSayfaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bildirimAksiyonSayfaService.GetCount(filter) : _bildirimAksiyonSayfaService.GetCount();
            List<BildirimAksiyonSayfa> d = _bildirimAksiyonSayfaService.GetListPagination(new PagingParams()
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
        public BildirimAksiyonSayfa Get(int id)
        {
            return _bildirimAksiyonSayfaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BildirimAksiyonSayfa bildirimAksiyonSayfa)
        {
            return _bildirimAksiyonSayfaService.Add(bildirimAksiyonSayfa);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BildirimAksiyonSayfa bildirimAksiyonSayfa)
        {
            return _bildirimAksiyonSayfaService.Update(bildirimAksiyonSayfa);
        }

        public int Delete(int id)
        {
            return _bildirimAksiyonSayfaService.DeleteSoft(id);
        }

        [Route("api/bildirimaksiyonsayfa/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bildirimAksiyonSayfaService.Delete(id);
        }
    }
}

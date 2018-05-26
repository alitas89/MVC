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
    public class BildirimIsTalebiSonucController : ApiController
    {
        IBildirimIsTalebiSonucService _bildirimIsTalebiSonucService;

        public BildirimIsTalebiSonucController(IBildirimIsTalebiSonucService bildirimIsTalebiSonucService)
        {
            _bildirimIsTalebiSonucService = bildirimIsTalebiSonucService;
        }

        // GET api/<controller>
        public IEnumerable<BildirimIsTalebiSonuc> Get()
        {
            return _bildirimIsTalebiSonucService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bildirimIsTalebiSonucService.GetCount(filter) : _bildirimIsTalebiSonucService.GetCount();
            List<BildirimIsTalebiSonuc> d = _bildirimIsTalebiSonucService.GetListPagination(new PagingParams()
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
        public BildirimIsTalebiSonuc Get(int id)
        {
            return _bildirimIsTalebiSonucService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BildirimIsTalebiSonuc bildirimIsTalebiSonuc)
        {
            return _bildirimIsTalebiSonucService.Add(bildirimIsTalebiSonuc);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BildirimIsTalebiSonuc bildirimIsTalebiSonuc)
        {
            return _bildirimIsTalebiSonucService.Update(bildirimIsTalebiSonuc);
        }

        public int Delete(int id)
        {
            return _bildirimIsTalebiSonucService.DeleteSoft(id);
        }

        [Route("api/bildirimıstalebisonuc/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bildirimIsTalebiSonucService.Delete(id);
        }

        [HttpGet]
        [Route("api/bildirimistalebisonuc/addbildirimistalebisonuc/{id}")]
        public int AddBildirimIsTalebiSonuc(int id)
        {
            return _bildirimIsTalebiSonucService.Add(new BildirimIsTalebiSonuc()
            {
                IsEmriNoID = id,
                Silindi = false
            });
        }
    }
}
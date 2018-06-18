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
    //**WEB API CONTROLLER - BildirimTrigger
    public class BildirimTriggerController : ApiController
    {
        IBildirimTriggerService _bildirimTriggerService;

        public BildirimTriggerController(IBildirimTriggerService bildirimTriggerService)
        {
            _bildirimTriggerService = bildirimTriggerService;
        }

        // GET api/<controller>
        public IEnumerable<BildirimTrigger> Get()
        {
            return _bildirimTriggerService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bildirimTriggerService.GetCount(filter) : _bildirimTriggerService.GetCount();
            List<BildirimTrigger> d = _bildirimTriggerService.GetListPagination(new PagingParams()
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
        public BildirimTrigger Get(int id)
        {
            return _bildirimTriggerService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BildirimTrigger bildirimTrigger)
        {
            return _bildirimTriggerService.Add(bildirimTrigger);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BildirimTrigger bildirimTrigger)
        {
            return _bildirimTriggerService.Update(bildirimTrigger);
        }

        public int Delete(int id)
        {
            return _bildirimTriggerService.DeleteSoft(id);
        }

        [Route("api/bildirimtrigger/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bildirimTriggerService.Delete(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - AlarmKosulTip

    public class AlarmKosulTipController : ApiController
    {
        IAlarmKosulTipService _alarmKosulTipService;

        public AlarmKosulTipController(IAlarmKosulTipService alarmKosulTipService)
        {
            _alarmKosulTipService = alarmKosulTipService;
        }

        // GET api/<controller>
        public IEnumerable<AlarmKosulTip> Get()
        {
            return _alarmKosulTipService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _alarmKosulTipService.GetCount(filter) : _alarmKosulTipService.GetCount();
            List<AlarmKosulTip> d = _alarmKosulTipService.GetListPagination(new PagingParams()
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
        public AlarmKosulTip Get(int id)
        {
            return _alarmKosulTipService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AlarmKosulTip alarmKosulTip)
        {
            return _alarmKosulTipService.Add(alarmKosulTip);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AlarmKosulTip alarmKosulTip)
        {
            return _alarmKosulTipService.Update(alarmKosulTip);
        }

        public int Delete(int id)
        {
            return _alarmKosulTipService.DeleteSoft(id);
        }

        [Route("api/alarmkosultip/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _alarmKosulTipService.Delete(id);
        }
    }
}

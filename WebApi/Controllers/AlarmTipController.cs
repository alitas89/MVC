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
    //**WEB API CONTROLLER - AlarmTip

    public class AlarmTipController : ApiController
    {
        IAlarmTipService _alarmTipService;

        public AlarmTipController(IAlarmTipService alarmTipService)
        {
            _alarmTipService = alarmTipService;
        }

        // GET api/<controller>
        [Route("api/alarmtip/getAll")]
        public IEnumerable<AlarmTip> Get()
        {
            return _alarmTipService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _alarmTipService.GetCount(filter) : _alarmTipService.GetCount();
            List<AlarmTip> d = _alarmTipService.GetListPagination(new PagingParams()
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
        [Route("api/alarmtip/getbyid/{id}")]
        public AlarmTip Get(int id)
        {
            return _alarmTipService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AlarmTip alarmTip)
        {
            return _alarmTipService.Add(alarmTip);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AlarmTip alarmTip)
        {
            return _alarmTipService.Update(alarmTip);
        }

        public int Delete(int id)
        {
            return _alarmTipService.DeleteSoft(id);
        }

        [Route("api/alarmtip/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _alarmTipService.Delete(id);
        }
    }
}

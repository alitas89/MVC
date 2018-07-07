using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - Alarm

    public class AlarmController : ApiController
    {
        IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        // GET api/<controller>
        public IEnumerable<Alarm> Get()
        {
            return _alarmService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _alarmService.GetCount(filter) : _alarmService.GetCount();
            List<AlarmDto> d = _alarmService.GetListPaginationDto(new PagingParams()
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
        public Alarm Get(int id)
        {
            return _alarmService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AlarmTemp alarmTemp)
        {
            return _alarmService.AddWithTransaction(alarmTemp.alarm, alarmTemp.listAlarmKosul);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AlarmTemp alarmTemp)
        {
            return _alarmService.UpdateWithTransaction(alarmTemp.alarm, alarmTemp.listAlarmKosul);
        }

        public int Delete(int id)
        {
            return _alarmService.DeleteSoft(id);
        }

        [Route("api/alarm/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _alarmService.Delete(id);
        }
    }
}

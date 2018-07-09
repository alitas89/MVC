using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - AlarmKosul

    public class AlarmKosulController : ApiController
    {
        IAlarmKosulService _alarmKosulService;

        public AlarmKosulController(IAlarmKosulService alarmKosulService)
        {
            _alarmKosulService = alarmKosulService;
        }

        // GET api/<controller>
        public IEnumerable<AlarmKosul> Get()
        {
            return _alarmKosulService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _alarmKosulService.GetCount(filter) : _alarmKosulService.GetCount();
            List<AlarmKosul> d = _alarmKosulService.GetListPagination(new PagingParams()
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
        public AlarmKosul Get(int id)
        {
            return _alarmKosulService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AlarmKosul alarmKosul)
        {
            return _alarmKosulService.Add(alarmKosul);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AlarmKosul alarmKosul)
        {
            return _alarmKosulService.Update(alarmKosul);
        }

        public int Delete(int id)
        {
            return _alarmKosulService.DeleteSoft(id);
        }

        [Route("api/alarmkosul/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _alarmKosulService.Delete(id);
        }

        [Route("api/alarmkosultip/getlistalarmkosulbyalarmid/{AlarmID}")]
        public List<AlarmKosulDto> GetListAlarmKosulByAlarmID(int AlarmID)
        {
            return _alarmKosulService.GetListAlarmKosulByAlarmID(AlarmID);
        }
    }
}

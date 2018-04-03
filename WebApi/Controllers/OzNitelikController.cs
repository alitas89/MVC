using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace WebApi.Controllers
{
    public class OzNitelikController : ApiController
    {
        IOzNitelikService _ozNitelikService;

        public OzNitelikController(IOzNitelikService ozNitelikService)
        {
            _ozNitelikService = ozNitelikService;
        }

        // GET api/<controller>
        public IEnumerable<OzNitelik> Get()
        {
            return _ozNitelikService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _ozNitelikService.GetCount(filter) : _ozNitelikService.GetCount();
            var d = _ozNitelikService.GetListPagination(new PagingParams()
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
        public OzNitelik Get(int id)
        {
            return _ozNitelikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]OzNitelikTemp ozNitelikTemp)
        {
            return _ozNitelikService.AddOzNitelik(ozNitelikTemp.VarlikSablonID,ozNitelikTemp.arrOzNitelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]OzNitelikTemp ozNitelikTemp)
        {
            return _ozNitelikService.UpdateOzNitelik(ozNitelikTemp.VarlikSablonID, ozNitelikTemp.arrOzNitelik);
        }

        public int Delete(int id)
        {
            return _ozNitelikService.DeleteSoft(id);
        }

        [Route("api/oznitelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _ozNitelikService.Delete(id);
        }

        // GET api/<controller>
        [Route("api/oznitelik/getlistbyvarliksablonid/{VarlikSablonID}")]
        [HttpGet]
        public IEnumerable<OzNitelikDto> GetListByVarlikSablonID(int VarlikSablonID)
        {
            return _ozNitelikService.GetList(VarlikSablonID);
        }

        [Route("api/oznitelik/getlistbyvarlikturuid/{VarlikTuruID}")]
        [HttpGet]
        public IEnumerable<OzNitelikDto> GetListByVarlikTuruID(int VarlikTuruID)
        {
            return _ozNitelikService.GetListByVarlikTuruID(VarlikTuruID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int VarlikSablonID, int offset, int limit, string filter = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0
                ? _ozNitelikService.GetCountDto(VarlikSablonID, filter)
                : _ozNitelikService.GetCountDto(VarlikSablonID);
            var d = _ozNitelikService.GetListPaginationDto(VarlikSablonID, new PagingParams()
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
    }
}
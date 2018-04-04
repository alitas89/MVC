using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Linq.Dynamic;
using System.Web.Http;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace WebApi.Controllers
{
    public class VarlikOzNitelikController : ApiController
    {
        IVarlikOzNitelikService _varlikOzNitelikService;

        public VarlikOzNitelikController(IVarlikOzNitelikService varlikOzNitelikService)
        {
            _varlikOzNitelikService = varlikOzNitelikService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikOzNitelik> Get()
        {
            return _varlikOzNitelikService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikOzNitelikService.GetCount(filter) : _varlikOzNitelikService.GetCount();
            var d = _varlikOzNitelikService.GetListPagination(new PagingParams()
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
        public VarlikOzNitelik Get(int id)
        {
            return _varlikOzNitelikService.GetById(id);
        }

        [Route("api/varlikoznitelik/getlistbyvarlikid/{VarlikID}")]
        [HttpGet]
        public IEnumerable<VarlikOzNitelik> GetListByVarlikID(int VarlikID)
        {
            return _varlikOzNitelikService.GetListByVarlikID(VarlikID);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikOzNitelikTemp varlikOzNitelikTemp)
        {
            return _varlikOzNitelikService.AddVarlikOzNitelik(varlikOzNitelikTemp.VarlikID,varlikOzNitelikTemp.arrVarlikOznitelik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikOzNitelikTemp varlikOzNitelikTemp)
        {
            return _varlikOzNitelikService.UpdateVarlikOzNitelik(varlikOzNitelikTemp.VarlikID, varlikOzNitelikTemp.arrVarlikOznitelik);
        }

        public int Delete(int id)
        {
            return _varlikOzNitelikService.DeleteSoft(id);
        }

        [Route("api/varlikoznitelik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikOzNitelikService.Delete(id);
        }
    }
}
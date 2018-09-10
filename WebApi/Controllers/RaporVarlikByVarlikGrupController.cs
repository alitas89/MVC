using BusinessLayer.Abstract.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;
using System.Data;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class RaporVarlikByVarlikGrupController : ApiController
    {
        IRaporVarlikByVarlikGrupService _varlikByVarlikGrupService;

        public RaporVarlikByVarlikGrupController(IRaporVarlikByVarlikGrupService varlikByVarlikGrupService)
        {
            _varlikByVarlikGrupService = varlikByVarlikGrupService;
        }

        // GET api/<controller>
        public IEnumerable<Varlik> Get()
        {
            return new List<Varlik>();

        }

        // GET api/<controller>
        public HttpResponseMessage Get(int VarlikGrupID, int offset, int limit, string filter = "", 
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikByVarlikGrupService.GetCountDtoByVarlikGrupID(VarlikGrupID, filter) : _varlikByVarlikGrupService.GetCountDtoByVarlikGrupID(VarlikGrupID);
            var d = _varlikByVarlikGrupService.GetListPaginationDtoByVarlikGrupID(VarlikGrupID, new PagingParams()
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

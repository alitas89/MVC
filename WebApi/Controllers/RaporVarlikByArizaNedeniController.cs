using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class RaporVarlikByArizaNedeniController : ApiController
    {
        IRaporVarlikByArizaNedeniService _varlikByArizaNedeniService;

        public RaporVarlikByArizaNedeniController(IRaporVarlikByArizaNedeniService varlikByArizaNedeniService)
        {
            _varlikByArizaNedeniService = varlikByArizaNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<Varlik> Get()
        {
            return new List<Varlik>();

        }

        // GET api/<controller>
        public HttpResponseMessage Get(int ArizaNedeniID, int offset, int limit, string filter = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikByArizaNedeniService.GetCountDtoByArizaNedeniID(ArizaNedeniID, filter) : _varlikByArizaNedeniService.GetCountDtoByArizaNedeniID(ArizaNedeniID);
            var d = _varlikByArizaNedeniService.GetListPaginationDtoByArizaNedeniID(ArizaNedeniID, new PagingParams()
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

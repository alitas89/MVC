using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class RaporArizaNedeniByVarlikController : ApiController
    {
        IRaporArizaNedeniByVarlikService _raporArizaNedeniByVarlikService;

        public RaporArizaNedeniByVarlikController(IRaporArizaNedeniByVarlikService raporArizaNedeniByVarlikService)
        {
            _raporArizaNedeniByVarlikService = raporArizaNedeniByVarlikService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaNedeni> Get()
        {
            return new List<ArizaNedeni>();

        }

        // GET api/<controller>
        public HttpResponseMessage Get(int VarlikID, int offset, int limit, string filter = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _raporArizaNedeniByVarlikService.GetCountDtoByVarlikID(VarlikID, filter) : _raporArizaNedeniByVarlikService.GetCountDtoByVarlikID(VarlikID);
            var d = _raporArizaNedeniByVarlikService.GetListPaginationDtoByVarlikID(VarlikID, new PagingParams()
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
